USE [master]
GO
CREATE DATABASE [QuanLyNgoaiNgu]
GO
USE [QuanLyNgoaiNgu]
GO

-- ==============================================================================
-- PHẦN 1: TẠO BẢNG (TABLES)
-- Cấu trúc các bảng đầy đủ thuộc tính để quản lý theo yêu cầu
-- ==============================================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 1. Bảng users: Quản lý đăng nhập và phân quyền chung
CREATE TABLE [dbo].[users](
	[user_id] [varchar](50) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](150) NOT NULL,
	[role] [varchar](30) NOT NULL, -- 'Admin', 'Teacher', 'Student'
	[email] [varchar](150) NULL,
	[created_date] [datetime] DEFAULT GETDATE(),
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED ([user_id] ASC)
) ON [PRIMARY]
GO

-- 2. Bảng teachers: Thông tin chi tiết giáo viên
CREATE TABLE [dbo].[teachers](
	[teacher_id] [varchar](50) NOT NULL,
	[user_id] [varchar](50) NULL,
	[full_name] [nvarchar](150) NULL,
	[specialization] [nvarchar](150) NULL,
	[phone] [varchar](20) NULL,
	[address] [nvarchar](250) NULL,
 CONSTRAINT [PK_teachers] PRIMARY KEY CLUSTERED ([teacher_id] ASC)
) ON [PRIMARY]
GO

-- 3. Bảng students: Thông tin chi tiết học viên
CREATE TABLE [dbo].[students](
	[student_id] [varchar](50) NOT NULL,
	[user_id] [varchar](50) NULL,
	[full_name] [nvarchar](150) NULL,
	[dob] [date] NULL,
	[address] [nvarchar](250) NULL,
	[phone] [varchar](20) NULL,
	[gender] [nvarchar](10) NULL,
 CONSTRAINT [PK_students] PRIMARY KEY CLUSTERED ([student_id] ASC)
) ON [PRIMARY]
GO

-- 4. Bảng courses: Thông tin khóa học
CREATE TABLE [dbo].[courses](
	[course_id] [varchar](50) NOT NULL,
	[course_name] [nvarchar](150) NULL,
	[teacher_id] [varchar](50) NULL,
	[tuition_fee] [float] NULL,
	[start_date] [date] NULL,
	[end_date] [date] NULL,
	[max_students] [int] NULL,
	[status] [varchar](50) NULL, -- 'Upcoming', 'Ongoing', 'Completed'
 CONSTRAINT [PK_courses] PRIMARY KEY CLUSTERED ([course_id] ASC)
) ON [PRIMARY]
GO

-- 5. Bảng enrollments: Lịch sử ghi danh (Flow Đăng ký)
CREATE TABLE [dbo].[enrollments](
	[enrollment_id] [varchar](50) NOT NULL,
	[course_id] [varchar](50) NULL,
	[student_id] [varchar](50) NULL,
	[enrollment_date] [datetime] DEFAULT GETDATE(),
	[status] [varchar](50) NULL, -- 'Registered', 'Studying', 'Completed', 'Dropped'
	[certificate_issued] [bit] NULL DEFAULT 0,
	[certificate_url] [varchar](250) NULL, -- Dùng export PDF chứng chỉ
 CONSTRAINT [PK_enrollments] PRIMARY KEY CLUSTERED ([enrollment_id] ASC)
) ON [PRIMARY]
GO

-- 6. Bảng invoices: Hóa đơn học phí
CREATE TABLE [dbo].[invoices](
	[invoice_id] [varchar](50) NOT NULL,
	[enrollment_id] [varchar](50) NULL,
	[amount] [float] NULL,
	[payment_date] [datetime] NULL,
	[status] [varchar](50) NULL, -- 'Unpaid', 'Paid'
 CONSTRAINT [PK_invoices] PRIMARY KEY CLUSTERED ([invoice_id] ASC)
) ON [PRIMARY]
GO

-- 7. Bảng exams: Thông tin kỳ thi (Xếp lớp, giữa kỳ, cuối kỳ)
CREATE TABLE [dbo].[exams](
	[exam_id] [varchar](50) NOT NULL,
	[course_id] [varchar](50) NULL,
	[exam_name] [nvarchar](150) NULL,
	[exam_type] [varchar](50) NULL, -- 'Placement', 'Midterm', 'Final'
	[exam_date] [datetime] NULL,
 CONSTRAINT [PK_exams] PRIMARY KEY CLUSTERED ([exam_id] ASC)
) ON [PRIMARY]
GO

-- 8. Bảng grades: Kết quả bài thi của học viên
CREATE TABLE [dbo].[grades](
	[grade_id] [varchar](50) NOT NULL,
	[exam_id] [varchar](50) NULL,
	[student_id] [varchar](50) NULL,
	[score] [float] NULL,
	[comments] [nvarchar](250) NULL,
 CONSTRAINT [PK_grades] PRIMARY KEY CLUSTERED ([grade_id] ASC)
) ON [PRIMARY]
GO

-- ==============================================================================
-- PHẦN 2: THIẾT LẬP KHÓA NGOẠI (FOREIGN KEYS)
-- ==============================================================================
ALTER TABLE [dbo].[teachers] WITH CHECK ADD CONSTRAINT [FK_teachers_users] FOREIGN KEY([user_id]) REFERENCES [dbo].[users] ([user_id])
ALTER TABLE [dbo].[students] WITH CHECK ADD CONSTRAINT [FK_students_users] FOREIGN KEY([user_id]) REFERENCES [dbo].[users] ([user_id])
ALTER TABLE [dbo].[courses] WITH CHECK ADD CONSTRAINT [FK_courses_teachers] FOREIGN KEY([teacher_id]) REFERENCES [dbo].[teachers] ([teacher_id])
ALTER TABLE [dbo].[enrollments] WITH CHECK ADD CONSTRAINT [FK_enrollments_courses] FOREIGN KEY([course_id]) REFERENCES [dbo].[courses] ([course_id])
ALTER TABLE [dbo].[enrollments] WITH CHECK ADD CONSTRAINT [FK_enrollments_students] FOREIGN KEY([student_id]) REFERENCES [dbo].[students] ([student_id])
ALTER TABLE [dbo].[invoices] WITH CHECK ADD CONSTRAINT [FK_invoices_enrollments] FOREIGN KEY([enrollment_id]) REFERENCES [dbo].[enrollments] ([enrollment_id])
ALTER TABLE [dbo].[exams] WITH CHECK ADD CONSTRAINT [FK_exams_courses] FOREIGN KEY([course_id]) REFERENCES [dbo].[courses] ([course_id])
ALTER TABLE [dbo].[grades] WITH CHECK ADD CONSTRAINT [FK_grades_exams] FOREIGN KEY([exam_id]) REFERENCES [dbo].[exams] ([exam_id])
ALTER TABLE [dbo].[grades] WITH CHECK ADD CONSTRAINT [FK_grades_students] FOREIGN KEY([student_id]) REFERENCES [dbo].[students] ([student_id])
GO

-- ==============================================================================
-- PHẦN 3: STORED PROCEDURES (DANH MỤC & CRUD CƠ BẢN)
-- Áp dụng kỹ thuật phân trang từ file BanOto.sql
-- ==============================================================================

-- 3.1. Tìm kiếm và phân trang học viên
CREATE PROCEDURE [dbo].[sp_student_search] (
    @page_index  INT, 
    @page_size   INT,
    @full_name   NVARCHAR(150),
    @phone       VARCHAR(20)
)
AS
BEGIN
    DECLARE @RecordCount BIGINT;
    IF(@page_size <> 0)
    BEGIN
        SET NOCOUNT ON;
        SELECT (ROW_NUMBER() OVER(ORDER BY full_name ASC)) AS RowNumber, 
               s.student_id, 
               s.full_name, 
               s.dob, 
               s.phone, 
               s.address,
               u.email
        INTO #Results1
        FROM [students] AS s
        LEFT JOIN [users] AS u ON s.user_id = u.user_id
        WHERE (@full_name = '' OR s.full_name LIKE N'%' + @full_name + '%') 
          AND (@phone = '' OR s.phone LIKE '%' + @phone + '%');                  
        
        SELECT @RecordCount = COUNT(*) FROM #Results1;
        
        SELECT *, @RecordCount AS RecordCount
        FROM #Results1
        WHERE RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
           OR @page_index = -1;
           
        DROP TABLE #Results1; 
    END;
    ELSE
    BEGIN
        SET NOCOUNT ON;
        SELECT (ROW_NUMBER() OVER(ORDER BY full_name ASC)) AS RowNumber, 
               s.student_id, 
               s.full_name, 
               s.dob, 
               s.phone, 
               s.address,
               u.email
        INTO #Results2
        FROM [students] AS s
        LEFT JOIN [users] AS u ON s.user_id = u.user_id
        WHERE (@full_name = '' OR s.full_name LIKE N'%' + @full_name + '%') 
          AND (@phone = '' OR s.phone LIKE '%' + @phone + '%');                        
        
        SELECT @RecordCount = COUNT(*) FROM #Results2;
        
        SELECT *, @RecordCount AS RecordCount FROM #Results2;
        DROP TABLE #Results2;
    END;
END;
GO

-- 3.2. Tìm kiếm và phân trang khóa học
CREATE PROCEDURE [dbo].[sp_course_search] (
    @page_index  INT, 
    @page_size   INT,
    @course_name NVARCHAR(150),
    @status      VARCHAR(50)
)
AS
BEGIN
    DECLARE @RecordCount BIGINT;
    IF(@page_size <> 0)
    BEGIN
        SET NOCOUNT ON;
        SELECT (ROW_NUMBER() OVER(ORDER BY c.start_date DESC)) AS RowNumber, 
               c.course_id, 
               c.course_name, 
               t.full_name AS teacher_name, 
               c.tuition_fee, 
               c.start_date, 
               c.end_date, 
               c.status
        INTO #Results1
        FROM [courses] AS c
        LEFT JOIN [teachers] AS t ON c.teacher_id = t.teacher_id
        WHERE (@course_name = '' OR c.course_name LIKE N'%' + @course_name + '%') 
          AND (@status = '' OR c.status = @status);                  
        
        SELECT @RecordCount = COUNT(*) FROM #Results1;
        
        SELECT *, @RecordCount AS RecordCount
        FROM #Results1
        WHERE RowNumber BETWEEN (@page_index - 1) * @page_size + 1 AND (((@page_index - 1) * @page_size + 1) + @page_size) - 1
           OR @page_index = -1;
           
        DROP TABLE #Results1; 
    END
    -- Phần ELSE xử lý tương tự như sp_student_search nếu page_size = 0
END;
GO

-- ==============================================================================
-- PHẦN 4: NGHIỆP VỤ (FLOW ĐĂNG KÝ, THI, CẤP CHỨNG CHỈ)
-- Áp dụng OPENJSON để xử lý Insert/Update hàng loạt từ JSON[cite: 1]
-- ==============================================================================

-- 4.1. Ghi danh học viên vào khóa học & Tạo hóa đơn (Import từ Excel dùng SP này)
CREATE PROCEDURE [dbo].[sp_enrollment_create_batch]
(
 @course_id VARCHAR(50), 
 @tuition_fee FLOAT,
 @listjson_students NVARCHAR(MAX) -- Cấu trúc JSON: [{"enrollment_id": "E01", "student_id": "S01"}][cite: 1]
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF(@listjson_students IS NOT NULL)
        BEGIN
            -- 1. Insert nhiều học viên vào lớp cùng lúc
            INSERT INTO enrollments (enrollment_id, course_id, student_id, enrollment_date, status, certificate_issued)
            SELECT 
                JSON_VALUE(p.value, '$.enrollment_id'), 
                @course_id, 
                JSON_VALUE(p.value, '$.student_id'), 
                GETDATE(), 
                'Registered',
                0
            FROM OPENJSON(@listjson_students) AS p;[cite: 1]

            -- 2. Tự động sinh hóa đơn thu học phí cho danh sách vừa đăng ký
            INSERT INTO invoices (invoice_id, enrollment_id, amount, payment_date, status)
            SELECT 
                NEWID(), -- Dùng NEWID() để sinh mã invoice tự động
                JSON_VALUE(p.value, '$.enrollment_id'), 
                @tuition_fee, 
                NULL, 
                'Unpaid'
            FROM OPENJSON(@listjson_students) AS p;[cite: 1]
        END;

        COMMIT TRANSACTION;
        SELECT 'Success' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- 4.2. Nhập điểm hàng loạt & Đánh giá cấp chứng chỉ hoàn thành
CREATE PROCEDURE [dbo].[sp_grades_update_and_certify]
(
 @exam_id VARCHAR(50),
 @listjson_grades NVARCHAR(MAX) -- JSON: [{"student_id":"S01", "score":8.5, "comments":"Good"}][cite: 1]
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Nhập điểm
        IF(@listjson_grades IS NOT NULL)
        BEGIN
            INSERT INTO grades (grade_id, exam_id, student_id, score, comments)
            SELECT 
                NEWID(), 
                @exam_id, 
                JSON_VALUE(p.value, '$.student_id'), 
                CAST(JSON_VALUE(p.value, '$.score') AS FLOAT), 
                JSON_VALUE(p.value, '$.comments')
            FROM OPENJSON(@listjson_grades) AS p;[cite: 1]
        END;

        -- 2. Kiểm tra nghiệp vụ cấp chứng chỉ (NFR)
        DECLARE @course_id VARCHAR(50);
        DECLARE @exam_type VARCHAR(50);
        
        -- Lấy thông tin bài thi hiện tại
        SELECT @course_id = course_id, @exam_type = exam_type FROM exams WHERE exam_id = @exam_id;

        -- Nếu là bài thi cuối kỳ (Final) và điểm >= 5.0 -> Đủ điều kiện pass
        IF (@exam_type = 'Final')
        BEGIN
            UPDATE e
            SET e.certificate_issued = 1,
                e.status = 'Completed',
                -- Tạo link định tuyến template PDF chờ backend gen
                e.certificate_url = '/exports/certificate/pdf/' + e.enrollment_id 
            FROM enrollments e
            JOIN grades g ON e.student_id = g.student_id
            WHERE g.exam_id = @exam_id 
              AND e.course_id = @course_id 
              AND g.score >= 5.0;
        END;

        COMMIT TRANSACTION;
        SELECT 'Success' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- 4.3. Báo cáo Dashboard quản lý lớp học (Số lớp, kết quả học viên)
CREATE PROCEDURE [dbo].[sp_report_dashboard]
(
    @start_date DATE = NULL,
    @end_date DATE = NULL
)
AS
BEGIN
    SELECT 
        c.course_id,
        c.course_name,
        t.full_name AS teacher_name,
        c.status,
        COUNT(e.enrollment_id) AS total_students,
        SUM(CASE WHEN e.certificate_issued = 1 THEN 1 ELSE 0 END) AS students_passed,
        SUM(CASE WHEN i.status = 'Paid' THEN i.amount ELSE 0 END) AS total_revenue
    FROM courses c
    LEFT JOIN teachers t ON c.teacher_id = t.teacher_id
    LEFT JOIN enrollments e ON c.course_id = e.course_id
    LEFT JOIN invoices i ON e.enrollment_id = i.enrollment_id
    WHERE (@start_date IS NULL OR c.start_date >= @start_date)
      AND (@end_date IS NULL OR c.start_date <= @end_date)
    GROUP BY c.course_id, c.course_name, t.full_name, c.status
    ORDER BY c.start_date DESC;
END;
GO