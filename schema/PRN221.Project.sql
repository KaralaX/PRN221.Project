USE [PRN221.Project]
GO
/****** Object:  Table [dbo].[AppointmentResults]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentResults](
	[AppointmentId] [uniqueidentifier] NOT NULL,
	[TreatmentPlan] [text] NULL,
	[TestResult] [text] NULL,
 CONSTRAINT [PK_AppointmentResults] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [uniqueidentifier] NOT NULL,
	[ScheduleId] [uniqueidentifier] NOT NULL,
	[ScheduledDateTime] [datetime2](7) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[PatientId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorServices]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorServices](
	[DoctorId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DoctorServices] PRIMARY KEY CLUSTERED 
(
	[DoctorId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalBills]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalBills](
	[AppointmentId] [uniqueidentifier] NOT NULL,
	[Price] [money] NULL,
	[CreatedDateTime] [datetime2](7) NULL,
	[UpdatedDateTime] [datetime2](7) NULL,
 CONSTRAINT [PK_MedicalBills] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientMedicalRecords]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientMedicalRecords](
	[PatientId] [uniqueidentifier] NOT NULL,
	[Allergies] [text] NULL,
	[Medications] [text] NULL,
 CONSTRAINT [PK_PatientMedicalRecords] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalInformations]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalInformations](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,
	[Dob] [datetime2](7) NULL,
	[Gender] [nvarchar](20) NULL,
	[Address] [nvarchar](100) NULL,
 CONSTRAINT [PK_PersonalInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [uniqueidentifier] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[DoctorId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[NumAppointment] [int] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceReview]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceReview](
	[Id] [uniqueidentifier] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](500) NOT NULL,
	[DoctorId] [uniqueidentifier] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
	[PatientId] [uniqueidentifier] NOT NULL,
	[AppointmentId] [uniqueidentifier] NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[UpdatedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ServiceReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staffs]    Script Date: 26/02/2024 17:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Staffs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppointmentResults]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentResults_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([Id])
GO
ALTER TABLE [dbo].[AppointmentResults] CHECK CONSTRAINT [FK_AppointmentResults_Appointments]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Patients]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Schedules] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Schedules]
GO
ALTER TABLE [dbo].[DoctorServices]  WITH CHECK ADD  CONSTRAINT [FK_DoctorServices_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[DoctorServices] CHECK CONSTRAINT [FK_DoctorServices_Doctors]
GO
ALTER TABLE [dbo].[DoctorServices]  WITH CHECK ADD  CONSTRAINT [FK_DoctorServices_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[DoctorServices] CHECK CONSTRAINT [FK_DoctorServices_Services]
GO
ALTER TABLE [dbo].[MedicalBills]  WITH CHECK ADD  CONSTRAINT [FK_MedicalBills_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([Id])
GO
ALTER TABLE [dbo].[MedicalBills] CHECK CONSTRAINT [FK_MedicalBills_Appointments]
GO
ALTER TABLE [dbo].[PatientMedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_PatientMedicalRecords_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[PatientMedicalRecords] CHECK CONSTRAINT [FK_PatientMedicalRecords_Patients]
GO
ALTER TABLE [dbo].[PersonalInformations]  WITH CHECK ADD  CONSTRAINT [FK_PersonalInformations_Doctors] FOREIGN KEY([Id])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[PersonalInformations] CHECK CONSTRAINT [FK_PersonalInformations_Doctors]
GO
ALTER TABLE [dbo].[PersonalInformations]  WITH CHECK ADD  CONSTRAINT [FK_PersonalInformations_Patients] FOREIGN KEY([Id])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[PersonalInformations] CHECK CONSTRAINT [FK_PersonalInformations_Patients]
GO
ALTER TABLE [dbo].[PersonalInformations]  WITH CHECK ADD  CONSTRAINT [FK_PersonalInformations_Staffs] FOREIGN KEY([Id])
REFERENCES [dbo].[Staffs] ([Id])
GO
ALTER TABLE [dbo].[PersonalInformations] CHECK CONSTRAINT [FK_PersonalInformations_Staffs]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Doctors]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Services]
GO
ALTER TABLE [dbo].[ServiceReview]  WITH CHECK ADD  CONSTRAINT [FK_ServiceReview_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([Id])
GO
ALTER TABLE [dbo].[ServiceReview] CHECK CONSTRAINT [FK_ServiceReview_Appointments]
GO
ALTER TABLE [dbo].[ServiceReview]  WITH CHECK ADD  CONSTRAINT [FK_ServiceReview_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[ServiceReview] CHECK CONSTRAINT [FK_ServiceReview_Doctors]
GO
ALTER TABLE [dbo].[ServiceReview]  WITH CHECK ADD  CONSTRAINT [FK_ServiceReview_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[ServiceReview] CHECK CONSTRAINT [FK_ServiceReview_Patients]
GO
ALTER TABLE [dbo].[ServiceReview]  WITH CHECK ADD  CONSTRAINT [FK_ServiceReview_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[ServiceReview] CHECK CONSTRAINT [FK_ServiceReview_Services]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Departments]
GO
