-- =============================================
-- TWORZENIE BAZY DANYCH
-- =============================================

-- Sprawdź czy baza danych istnieje i usuń ją jeśli tak
IF
    EXISTS (SELECT name
            FROM sys.databases
            WHERE name = 'PlannerDB')
    BEGIN
        ALTER
            DATABASE PlannerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP
            DATABASE PlannerDB;
    END

-- Utwórz nową bazę danych
CREATE
    DATABASE PlannerDB;
GO

-- Przełącz się na nową bazę danych
USE PlannerDB;
GO

-- =============================================
-- USUWANIE WSZYSTKICH TABEL (w odpowiedniej kolejności)
-- =============================================

-- Usuń tabele w odwrotnej kolejności zależności
DROP TABLE IF EXISTS Messages;
DROP TABLE IF EXISTS Notifications;
DROP TABLE IF EXISTS Reservations;
DROP TABLE IF EXISTS EventScheduleStaff;
DROP TABLE IF EXISTS EventSchedules;
DROP TABLE IF EXISTS EventTypes;
DROP TABLE IF EXISTS StaffAvailability;
DROP TABLE IF EXISTS StaffSpecializations;
DROP TABLE IF EXISTS Specializations;
DROP TABLE IF EXISTS Participants;
DROP TABLE IF EXISTS Staff;
DROP TABLE IF EXISTS Receptions;
DROP TABLE IF EXISTS CompanyHierarchy;
DROP TABLE IF EXISTS Companies;

-- =============================================
-- TWORZENIE TABEL OD NOWA
-- =============================================

-- Tabela Companies (podstawowa) - rozszerzona o funkcjonalność recepcji
CREATE TABLE Companies
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(255) NOT NULL UNIQUE,
    TaxCode      NCHAR(20)     NOT NULL,
    Street       NVARCHAR(255) NOT NULL,
    City         NVARCHAR(100) NOT NULL,
    PostalCode   NCHAR(10)     NOT NULL,
    Phone        NCHAR(20)     NOT NULL UNIQUE,
    Email        NVARCHAR(255) NOT NULL UNIQUE,
    IsParentNode BIT                          DEFAULT 0,
    IsReception  BIT                          DEFAULT 0,
    CreatedAt    DATETIME                     DEFAULT GETUTCDATE()
);

-- Tabela CompanyHierarchy (hierarchia firm i recepcji)
CREATE TABLE CompanyHierarchy
(
    CompanyId       UNIQUEIDENTIFIER PRIMARY KEY,
    ParentCompanyId UNIQUEIDENTIFIER,
    CONSTRAINT fk_ch_company FOREIGN KEY (CompanyId) 
            REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_ch_parent FOREIGN KEY (ParentCompanyId) 
            REFERENCES Companies (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela Staff (personel)
CREATE TABLE Staff
(
    Id        UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId UNIQUEIDENTIFIER NOT NULL,
    Role      NVARCHAR(50)     NOT NULL,
    Email     NVARCHAR(255)    NOT NULL UNIQUE,
    Password  NVARCHAR(255)    NOT NULL,
    FirstName NVARCHAR(100)    NOT NULL,
    LastName  NVARCHAR(100)    NOT NULL,
    Phone     NVARCHAR(30)     NOT NULL UNIQUE,
    CreatedAt DATETIME                     DEFAULT GETUTCDATE(),
    IsDeleted BIT                          DEFAULT 0,
    CONSTRAINT fk_staff_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Tabela Participants (uczestnicy)
CREATE TABLE Participants
(
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId   UNIQUEIDENTIFIER NOT NULL,
    Email       NVARCHAR(255)    NOT NULL,
    FirstName   NVARCHAR(100)    NOT NULL,
    LastName    NVARCHAR(100)    NOT NULL,
    Phone       NVARCHAR(30)     NOT NULL,
    GdprConsent BIT              NOT NULL,
    CreatedAt   DATETIME                     DEFAULT GETUTCDATE(),
    CONSTRAINT fk_participants_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Tabela Specializations (specjalizacje)
CREATE TABLE Specializations
(
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId   UNIQUEIDENTIFIER NOT NULL,
    Name        NVARCHAR(100)    NOT NULL,
    Description NVARCHAR(MAX),
    CONSTRAINT fk_specializations_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Tabela StaffSpecializations (specjalizacje personelu)
CREATE TABLE StaffSpecializations
(
    Id               UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId        UNIQUEIDENTIFIER NOT NULL,
    StaffId          UNIQUEIDENTIFIER NOT NULL,
    SpecializationId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT fk_staffspec_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_staffspec_staff FOREIGN KEY (StaffId)
        REFERENCES Staff (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_staffspec_specialization FOREIGN KEY (SpecializationId)
        REFERENCES Specializations (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela StaffAvailability (dostępność personelu)
CREATE TABLE StaffAvailability
(
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId   UNIQUEIDENTIFIER NOT NULL,
    StaffId     UNIQUEIDENTIFIER NOT NULL,
    Date        DATE             NOT NULL,
    StartTime   DATETIME         NOT NULL,
    EndTime     DATETIME         NOT NULL,
    IsAvailable BIT                          DEFAULT 1,
    CONSTRAINT fk_staffavail_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_staffavail_staff FOREIGN KEY (StaffId)
        REFERENCES Staff (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Tabela EventTypes (typy wydarzeń)
CREATE TABLE EventTypes
(
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(100)    NOT NULL,
    Description     NVARCHAR(MAX),
    Duration        INT              NOT NULL,
    Price           DECIMAL(10, 2)   NOT NULL,
    MaxParticipants INT,
    MinStaff        INT                          DEFAULT 1,
    CONSTRAINT fk_eventtypes_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Tabela EventSchedules (harmonogramy wydarzeń)
CREATE TABLE EventSchedules
(
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId   UNIQUEIDENTIFIER NOT NULL,
    EventTypeId UNIQUEIDENTIFIER NOT NULL,
    PlaceName   NVARCHAR(255)    NOT NULL,
    StartTime   DATETIME         NOT NULL,
    CreatedAt   DATETIME                     DEFAULT GETUTCDATE(),
    Status      NVARCHAR(20)                 DEFAULT 'Active',
    CONSTRAINT fk_eventschedules_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_eventschedules_eventtype FOREIGN KEY (EventTypeId)
        REFERENCES EventTypes (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Indeks na EventSchedules
CREATE INDEX idx_event_schedule_datetime ON EventSchedules (StartTime);

-- Tabela EventScheduleStaff (personel przypisany do wydarzeń)
CREATE TABLE EventScheduleStaff
(
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    EventScheduleId UNIQUEIDENTIFIER NOT NULL,
    StaffId         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT fk_eventstaff_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_eventstaff_schedule FOREIGN KEY (EventScheduleId)
        REFERENCES EventSchedules (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_eventstaff_staff FOREIGN KEY (StaffId)
        REFERENCES Staff (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela Reservations (rezerwacje)
CREATE TABLE Reservations
(
    Id               UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId        UNIQUEIDENTIFIER NOT NULL,
    ParticipantId    UNIQUEIDENTIFIER NOT NULL,
    EventScheduleId  UNIQUEIDENTIFIER NOT NULL,
    ParticipantCount INT                          DEFAULT 1,
    Status           NVARCHAR(20)                 DEFAULT 'Confirmed',
    Notes            NVARCHAR(MAX),
    CreatedAt        DATETIME                     DEFAULT GETUTCDATE(),
    CancelledAt      DATETIME         NULL,
    PaidAt           DATETIME         NULL,
    CONSTRAINT fk_reservations_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_reservations_participant FOREIGN KEY (ParticipantId)
        REFERENCES Participants (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_reservations_eventschedule FOREIGN KEY (EventScheduleId)
        REFERENCES EventSchedules (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela Notifications (powiadomienia)
CREATE TABLE Notifications
(
    Id            UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId     UNIQUEIDENTIFIER NOT NULL,
    ReservationId UNIQUEIDENTIFIER NOT NULL,
    EmailStatus   NVARCHAR(20)                 DEFAULT 'Pending',
    SmsStatus     NVARCHAR(20)                 DEFAULT 'Pending',
    EmailSentAt   DATETIME         NULL,
    SmsSentAt     DATETIME         NULL,
    EmailContent  NVARCHAR(MAX),
    SmsContent    NVARCHAR(MAX),
    CONSTRAINT fk_notifications_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_notifications_reservation FOREIGN KEY (ReservationId)
        REFERENCES Reservations (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela Messages (wiadomości)
CREATE TABLE Messages
(
    Id         UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CompanyId  UNIQUEIDENTIFIER NOT NULL,
    SenderId   UNIQUEIDENTIFIER,
    ReceiverId UNIQUEIDENTIFIER,
    Content    NVARCHAR(MAX)    NOT NULL,
    CreatedAt  DATETIME                     DEFAULT GETUTCDATE(),
    CONSTRAINT fk_messages_company FOREIGN KEY (CompanyId)
        REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT fk_messages_sender FOREIGN KEY (SenderId)
        REFERENCES Staff (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_messages_receiver FOREIGN KEY (ReceiverId)
        REFERENCES Staff (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- =============================================
-- INDEKSY DODATKOWE DLA OPTYMALIZACJI
-- =============================================

-- Indeks na flagę IsReception dla szybkiego wyszukiwania recepcji
CREATE INDEX idx_companies_is_reception ON Companies (IsReception);

-- Indeks na CompanyHierarchy dla zapytań hierarchicznych
CREATE INDEX idx_company_hierarchy_parent ON CompanyHierarchy (ParentCompanyId);

-- =============================================
-- PRZYKŁADOWE ZAPYTANIA POMOCNICZE
-- =============================================

-- Przykład: Znajdź wszystkie recepcje należące do danej firmy
-- SELECT c.* FROM Companies c
-- INNER JOIN CompanyHierarchy ch ON c.Id = ch.CompanyId
-- WHERE ch.ParentCompanyId = @ParentCompanyId AND c.IsReception = 1;

-- Przykład: Znajdź firmę główną dla danej recepcji
-- SELECT parent.* FROM Companies parent
-- INNER JOIN CompanyHierarchy ch ON parent.Id = ch.ParentCompanyId
-- WHERE ch.CompanyId = @ReceptionId;

-- =============================================
-- KOMUNIKAT KOŃCOWY
-- =============================================
PRINT
    'Baza danych została pomyślnie utworzona z nową strukturą!';