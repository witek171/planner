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
DROP TABLE IF EXISTS CompanyHierarchies;
DROP TABLE IF EXISTS Companies;

-- =============================================
-- TWORZENIE TABEL OD NOWA
-- =============================================

-- Tabela Companies (podstawowa) - rozszerzona o funkcjonalność recepcji
CREATE TABLE Companies
(
	Id           UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Name         NVARCHAR(255) NOT NULL,
	TaxCode      NVARCHAR(20)  NOT NULL,
	Street       NVARCHAR(255) NOT NULL,
	City         NVARCHAR(100) NOT NULL,
	PostalCode   NVARCHAR(10)  NOT NULL,
	Phone        NVARCHAR(20)  NOT NULL,
	Email        NVARCHAR(255) NOT NULL,
	IsParentNode BIT                          DEFAULT 0,
	IsReception  BIT                          DEFAULT 0,
	CreatedAt    DATETIME                     DEFAULT GETUTCDATE()
);

-- Tabela CompanyHierarchies (hierarchia firm i recepcji)
CREATE TABLE CompanyHierarchies
(
	CompanyId       UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	ParentCompanyId UNIQUEIDENTIFIER             NOT NULL,
	CONSTRAINT fk_ch_company FOREIGN KEY (CompanyId)
		REFERENCES Companies (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_ch_parent FOREIGN KEY (ParentCompanyId)
		REFERENCES Companies (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela Staff (personel)
CREATE TABLE Staff
(
	Id        UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	CompanyId UNIQUEIDENTIFIER NOT NULL,
	Role      NVARCHAR(50)     NOT NULL,
	Email     NVARCHAR(255)    NOT NULL,
	Password  NVARCHAR(255)    NOT NULL,
	FirstName NVARCHAR(100)    NOT NULL,
	LastName  NVARCHAR(100)    NOT NULL,
	Phone     NVARCHAR(30)     NOT NULL,
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
	StaffMemberId    UNIQUEIDENTIFIER NOT NULL,
	SpecializationId UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT fk_staffspec_company FOREIGN KEY (CompanyId)
		REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT fk_staffspec_staff FOREIGN KEY (StaffMemberId)
		REFERENCES Staff (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_staffspec_specialization FOREIGN KEY (SpecializationId)
		REFERENCES Specializations (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela StaffAvailability (dostępność personelu)
CREATE TABLE StaffAvailability
(
	Id            UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	CompanyId     UNIQUEIDENTIFIER NOT NULL,
	StaffMemberId UNIQUEIDENTIFIER NOT NULL,
	Date          DATE             NOT NULL,
	StartTime     DATETIME         NOT NULL,
	EndTime       DATETIME         NOT NULL,
	IsAvailable   BIT              NOT NULL,
	CONSTRAINT fk_staffavail_company FOREIGN KEY (CompanyId)
		REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT fk_staffavail_staff FOREIGN KEY (StaffMemberId)
		REFERENCES Staff (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
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
	IsDeleted       BIT                          DEFAULT 0,
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
	StaffMemberId   UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT fk_eventstaff_company FOREIGN KEY (CompanyId)
		REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT fk_eventstaff_schedule FOREIGN KEY (EventScheduleId)
		REFERENCES EventSchedules (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_eventstaff_staff FOREIGN KEY (StaffMemberId)
		REFERENCES Staff (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Tabela Reservations (rezerwacje)
CREATE TABLE Reservations
(
	Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	CompanyId       UNIQUEIDENTIFIER NOT NULL,
	EventScheduleId UNIQUEIDENTIFIER NOT NULL,
	Status          NVARCHAR(20)                 DEFAULT 'Confirmed',
	Notes           NVARCHAR(MAX),
	CreatedAt       DATETIME                     DEFAULT GETUTCDATE(),
	CancelledAt     DATETIME         NULL,
	PaidAt          DATETIME         NULL,
	CONSTRAINT fk_reservations_company FOREIGN KEY (CompanyId)
		REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT fk_reservations_eventschedule FOREIGN KEY (EventScheduleId)
		REFERENCES EventSchedules (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- NOWA TABELA: ReservationParticipants (tabela asocjacyjna)
CREATE TABLE ReservationParticipants
(
	Id            UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	CompanyId     UNIQUEIDENTIFIER NOT NULL,
	ReservationId UNIQUEIDENTIFIER NOT NULL,
	ParticipantId UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT fk_resparticipants_company FOREIGN KEY (CompanyId)
		REFERENCES Companies (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT fk_resparticipants_reservation FOREIGN KEY (ReservationId)
		REFERENCES Reservations (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT fk_resparticipants_participant FOREIGN KEY (ParticipantId)
		REFERENCES Participants (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
	-- Unikatowość: jeden uczestnik może być tylko raz w danej rezerwacji
	CONSTRAINT uk_reservation_participant UNIQUE (ReservationId, ParticipantId)
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

-- Indeks na CompanyHierarchies dla zapytań hierarchicznych
CREATE INDEX idx_company_hierarchy_parent ON CompanyHierarchies (ParentCompanyId);

-- Indeks na ReservationParticipants dla szybkiego wyszukiwania uczestników rezerwacji
CREATE INDEX idx_reservation_participants_reservation ON ReservationParticipants (ReservationId);
CREATE INDEX idx_reservation_participants_participant ON ReservationParticipants (ParticipantId);

-- =============================================
-- PRZYKŁADOWE ZAPYTANIA POMOCNICZE
-- =============================================

-- Przykład: Znajdź wszystkie recepcje należące do danej firmy
-- SELECT c.* FROM Companies c
-- INNER JOIN CompanyHierarchies ch ON c.Id = ch.CompanyId
-- WHERE ch.ParentCompanyId = @ParentCompanyId AND c.IsReception = 1;

-- Przykład: Znajdź firmę główną dla danej recepcji
-- SELECT parent.* FROM Companies parent
-- INNER JOIN CompanyHierarchies ch ON parent.Id = ch.ParentCompanyId
-- WHERE ch.CompanyId = @ReceptionId;

-- =============================================
-- TRIGGER: usuwa powiązane dane i na końcu usuwa firmę
-- (kolejność dobrana tak, by nie łamać ograniczeń FK)
-- =============================================

IF OBJECT_ID('dbo.trg_delete_company', 'TR') IS NOT NULL
	DROP TRIGGER dbo.trg_delete_company;
GO

CREATE TRIGGER dbo.trg_delete_company
	ON dbo.Companies
	INSTEAD OF DELETE
	AS
BEGIN
	SET NOCOUNT ON;

	-- Najpierw usuń tabele zależne od innych tabel

	-- Usuń wiadomości
	DELETE
	FROM dbo.Messages
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń powiadomienia
	DELETE
	FROM dbo.Notifications
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń uczestników rezerwacji (NOWA TABELA)
	DELETE
	FROM dbo.ReservationParticipants
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń rezerwacje
	DELETE
	FROM dbo.Reservations
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń przypisania personelu do wydarzeń
	DELETE
	FROM dbo.EventScheduleStaff
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń harmonogramy wydarzeń
	DELETE
	FROM dbo.EventSchedules
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń typy wydarzeń
	DELETE
	FROM dbo.EventTypes
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń dostępność personelu
	DELETE
	FROM dbo.StaffAvailability
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń specjalizacje personelu
	DELETE
	FROM dbo.StaffSpecializations
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń specjalizacje
	DELETE
	FROM dbo.Specializations
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń uczestników
	DELETE
	FROM dbo.Participants
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń personel
	DELETE
	FROM dbo.Staff
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń hierarchię firm (najpierw gdzie firma jest dzieckiem)
	DELETE
	FROM dbo.CompanyHierarchies
	WHERE CompanyId IN (SELECT Id FROM deleted);

	-- Usuń hierarchię firm (gdzie firma jest rodzicem)
	DELETE
	FROM dbo.CompanyHierarchies
	WHERE ParentCompanyId IN (SELECT Id FROM deleted);

	-- Na końcu usuń samą firmę
	DELETE
	FROM dbo.Companies
	WHERE Id IN (SELECT Id FROM deleted);

END;
GO

-- =============================================
-- KOMUNIKAT KOŃCOWY
-- =============================================
PRINT
	'Baza danych została pomyślnie utworzona z nową strukturą!';