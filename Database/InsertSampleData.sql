-- =============================================
-- WYPEŁNIANIE BAZY DANYCH PRZYKŁADOWYMI DANYMI 
-- =============================================

-- =============================================
-- 1. Companies - dodanie firm (klubów sportowych)
-- =============================================

-- SportFit Group
DECLARE @MainCompanyId UNIQUEIDENTIFIER = NEWID();
DECLARE @Branch1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Branch2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Branch3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Branch4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Branch5Id UNIQUEIDENTIFIER = NEWID();

-- FitZone Network
DECLARE @FitZoneMainId UNIQUEIDENTIFIER = NEWID();
DECLARE @FitZone1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @FitZone2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @FitZone3Id UNIQUEIDENTIFIER = NEWID();

-- AquaFit Centers
DECLARE @AquaFitMainId UNIQUEIDENTIFIER = NEWID();
DECLARE @AquaFit1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @AquaFit2Id UNIQUEIDENTIFIER = NEWID();

-- PowerGym Chain
DECLARE @PowerGymMainId UNIQUEIDENTIFIER = NEWID();
DECLARE @PowerGym1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @PowerGym2Id UNIQUEIDENTIFIER = NEWID();

-- FlexYoga Studios
DECLARE @FlexYogaMainId UNIQUEIDENTIFIER = NEWID();
DECLARE @FlexYoga1Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO Companies (Id, Name, TaxCode, Street, City, PostalCode, PhoneNumber, IsParentNode)
VALUES
    -- SportFit Group
    (@MainCompanyId, 'SportFit Group', 'PL1234567890', 'Sportowa 1', 'Warszawa', '00-001', '+48123456789', 1),
    (@Branch1Id, 'SportFit Centrum', 'PL2345678901', 'Centralna 10', 'Warszawa', '00-002', '+48234567890', 0),
    (@Branch2Id, 'SportFit Południe', 'PL3456789012', 'Południowa 20', 'Kraków', '30-001', '+48345678901', 0),
    (@Branch3Id, 'SportFit Północ', 'PL4567890123', 'Północna 30', 'Gdańsk', '80-001', '+48456789012', 0),
    (@Branch4Id, 'SportFit Wschód', 'PL5678901234', 'Wschodnia 40', 'Lublin', '20-001', '+48567890123', 0),
    (@Branch5Id, 'SportFit Zachód', 'PL6789012345', 'Zachodnia 50', 'Wrocław', '50-001', '+48678901234', 0),

    -- FitZone Network
    (@FitZoneMainId, 'FitZone Network', 'PL7890123456', 'Fitness 5', 'Warszawa', '02-001', '+48789012345', 1),
    (@FitZone1Id, 'FitZone Mokotów', 'PL8901234567', 'Mokotowska 15', 'Warszawa', '02-002', '+48890123456', 0),
    (@FitZone2Id, 'FitZone Katowice', 'PL9012345678', 'Śląska 25', 'Katowice', '40-001', '+48901234567', 0),
    (@FitZone3Id, 'FitZone Poznań', 'PL0123456789', 'Wielkopolska 35', 'Poznań', '60-001', '+48012345678', 0),

    -- AquaFit Centers
    (@AquaFitMainId, 'AquaFit Centers', 'PL1357924680', 'Wodna 8', 'Gdynia', '81-001', '+48135792468', 1),
    (@AquaFit1Id, 'AquaFit Marina', 'PL2468135790', 'Portowa 12', 'Gdynia', '81-002', '+48246813579', 0),
    (@AquaFit2Id, 'AquaFit Sopot', 'PL3579246801', 'Plażowa 7', 'Sopot', '81-700', '+48357924680', 0),

    -- PowerGym Chain
    (@PowerGymMainId, 'PowerGym Chain', 'PL4680357912', 'Siłowa 3', 'Łódź', '90-001', '+48468035791', 1),
    (@PowerGym1Id, 'PowerGym Center', 'PL5791468023', 'Centralna 45', 'Łódź', '90-002', '+48579146802', 0),
    (@PowerGym2Id, 'PowerGym Bydgoszcz', 'PL6802579134', 'Kujawska 18', 'Bydgoszcz', '85-001', '+48680257913', 0),

    -- FlexYoga Studios
    (@FlexYogaMainId, 'FlexYoga Studios', 'PL7913680245', 'Relaksacyjna 22', 'Warszawa', '01-001', '+48791368024', 1),
    (@FlexYoga1Id, 'FlexYoga Wilanów', 'PL8024791356', 'Spokójna 33', 'Warszawa', '02-958', '+48802479135', 0);

-- =============================================
-- 2. CompanyHierarchy - dodanie hierarchii firm
-- =============================================

INSERT INTO CompanyHierarchy (CompanyId, ParentCompanyId)
VALUES
    -- SportFit Group
    (@MainCompanyId, NULL),
    (@Branch1Id, @MainCompanyId),
    (@Branch2Id, @MainCompanyId),
    (@Branch3Id, @MainCompanyId),
    (@Branch4Id, @MainCompanyId),
    (@Branch5Id, @MainCompanyId),

    -- FitZone Network
    (@FitZoneMainId, NULL),
    (@FitZone1Id, @FitZoneMainId),
    (@FitZone2Id, @FitZoneMainId),
    (@FitZone3Id, @FitZoneMainId),

    -- AquaFit Centers
    (@AquaFitMainId, NULL),
    (@AquaFit1Id, @AquaFitMainId),
    (@AquaFit2Id, @AquaFitMainId),

    -- PowerGym Chain
    (@PowerGymMainId, NULL),
    (@PowerGym1Id, @PowerGymMainId),
    (@PowerGym2Id, @PowerGymMainId),

    -- FlexYoga Studios
    (@FlexYogaMainId, NULL),
    (@FlexYoga1Id, @FlexYogaMainId);

-- =============================================
-- 3. Receptions - dodanie recepcji
-- =============================================

DECLARE @Reception1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reception13Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO Receptions (Id, CompanyId, Address, Phone)
VALUES
    -- SportFit Group
    (@Reception1Id, @Branch1Id, 'Centralna 10, Warszawa - parter', '+48234567890'),
    (@Reception2Id, @Branch2Id, 'Południowa 20, Kraków - parter', '+48345678901'),
    (@Reception3Id, @Branch3Id, 'Północna 30, Gdańsk - parter', '+48456789012'),
    (@Reception4Id, @Branch4Id, 'Wschodnia 40, Lublin - parter', '+48567890123'),
    (@Reception5Id, @Branch5Id, 'Zachodnia 50, Wrocław - parter', '+48678901234'),

    -- FitZone Network
    (@Reception6Id, @FitZone1Id, 'Mokotowska 15, Warszawa - I piętro', '+48890123456'),
    (@Reception7Id, @FitZone2Id, 'Śląska 25, Katowice - parter', '+48901234567'),
    (@Reception8Id, @FitZone3Id, 'Wielkopolska 35, Poznań - parter', '+48012345678'),

    -- AquaFit Centers
    (@Reception9Id, @AquaFit1Id, 'Portowa 12, Gdynia - parter', '+48246813579'),
    (@Reception10Id, @AquaFit2Id, 'Plażowa 7, Sopot - parter', '+48357924680'),

    -- PowerGym Chain
    (@Reception11Id, @PowerGym1Id, 'Centralna 45, Łódź - parter', '+48579146802'),
    (@Reception12Id, @PowerGym2Id, 'Kujawska 18, Bydgoszcz - parter', '+48680257913'),

    -- FlexYoga Studios
    (@Reception13Id, @FlexYoga1Id, 'Spokójna 33, Warszawa - II piętro', '+48802479135');

-- =============================================
-- 4. Staff - dodanie personelu (POPRAWIONE NUMERY)
-- =============================================

-- Pracownicy recepcji
DECLARE @StaffRec1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @StaffRec13Id UNIQUEIDENTIFIER = NEWID();

-- Trenerzy
DECLARE @Trainer1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer13Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer14Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer15Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer16Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer17Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer18Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer19Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer20Id UNIQUEIDENTIFIER = NEWID();

-- Managerowie
DECLARE @Manager1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Manager10Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO Staff (Id, ReceptionId, Role, Email, PasswordHash, FirstName, LastName, Phone)
VALUES
    -- Pracownicy recepcji SportFit
    (@StaffRec1Id, @Reception1Id, 'ReceptionEmployee', 'kowalska@sportfit.pl', 'password123', 'Anna', 'Kowalska', '+48700100101'),
    (@StaffRec2Id, @Reception2Id, 'ReceptionEmployee', 'nowak@sportfit.pl', 'password123', 'Barbara', 'Nowak', '+48700100102'),
    (@StaffRec3Id, @Reception3Id, 'ReceptionEmployee', 'wisniewska@sportfit.pl', 'password123', 'Celina', 'Wiśniewska', '+48700100103'),
    (@StaffRec4Id, @Reception4Id, 'ReceptionEmployee', 'kaminska@sportfit.pl', 'password123', 'Diana', 'Kamińska', '+48700100104'),
    (@StaffRec5Id, @Reception5Id, 'ReceptionEmployee', 'lewandowska@sportfit.pl', 'password123', 'Ewa', 'Lewandowska', '+48700100105'),

    -- Pracownicy recepcji FitZone
    (@StaffRec6Id, @Reception6Id, 'ReceptionEmployee', 'zielinska@fitzone.pl', 'password123', 'Fatima', 'Zielińska', '+48700100106'),
    (@StaffRec7Id, @Reception7Id, 'ReceptionEmployee', 'szymanska@fitzone.pl', 'password123', 'Gabriela', 'Szymańska', '+48700100107'),
    (@StaffRec8Id, @Reception8Id, 'ReceptionEmployee', 'wojcik@fitzone.pl', 'password123', 'Hanna', 'Wójcik', '+48700100108'),

    -- Pracownicy recepcji AquaFit
    (@StaffRec9Id, @Reception9Id, 'ReceptionEmployee', 'kowalczyk@aquafit.pl', 'password123', 'Irena', 'Kowalczyk', '+48700100109'),
    (@StaffRec10Id, @Reception10Id, 'ReceptionEmployee', 'kozlowska@aquafit.pl', 'password123', 'Justyna', 'Kozłowska', '+48700100110'),

    -- Pracownicy recepcji PowerGym
    (@StaffRec11Id, @Reception11Id, 'ReceptionEmployee', 'jankowska@powergym.pl', 'password123', 'Klara', 'Jankowska', '+48700100111'),
    (@StaffRec12Id, @Reception12Id, 'ReceptionEmployee', 'zawadzka@powergym.pl', 'password123', 'Laura', 'Zawadzka', '+48700100112'),

    -- Pracownicy recepcji FlexYoga
    (@StaffRec13Id, @Reception13Id, 'ReceptionEmployee', 'mazur@flexyoga.pl', 'password123', 'Monika', 'Mazur', '+48700100113'),

    -- Trenerzy SportFit
    (@Trainer1Id, @Reception1Id, 'Trainer', 'malinowski@sportfit.pl', 'password123', 'Dariusz', 'Malinowski', '+48700200201'),
    (@Trainer2Id, @Reception1Id, 'Trainer', 'jablonska@sportfit.pl', 'password123', 'Ewa', 'Jabłońska', '+48700200202'),
    (@Trainer3Id, @Reception2Id, 'Trainer', 'kowalczyk@sportfit.pl', 'password123', 'Filip', 'Kowalczyk', '+48700200203'),
    (@Trainer4Id, @Reception2Id, 'Trainer', 'lewandowska2@sportfit.pl', 'password123', 'Grażyna', 'Lewandowska', '+48700200204'),
    (@Trainer5Id, @Reception3Id, 'Trainer', 'zielinski@sportfit.pl', 'password123', 'Henryk', 'Zieliński', '+48700200205'),
    (@Trainer6Id, @Reception3Id, 'Trainer', 'szymanska2@sportfit.pl', 'password123', 'Iwona', 'Szymańska', '+48700200206'),
    (@Trainer7Id, @Reception4Id, 'Trainer', 'borkowski@sportfit.pl', 'password123', 'Jacek', 'Borkowski', '+48700200207'),
    (@Trainer8Id, @Reception5Id, 'Trainer', 'krawczyk@sportfit.pl', 'password123', 'Kinga', 'Krawczyk', '+48700200208'),

    -- Trenerzy FitZone
    (@Trainer9Id, @Reception6Id, 'Trainer', 'nowicki@fitzone.pl', 'password123', 'Łukasz', 'Nowicki', '+48700200209'),
    (@Trainer10Id, @Reception6Id, 'Trainer', 'pawlak@fitzone.pl', 'password123', 'Marta', 'Pawlak', '+48700200210'),
    (@Trainer11Id, @Reception7Id, 'Trainer', 'michalski@fitzone.pl', 'password123', 'Norbert', 'Michalski', '+48700200211'),
    (@Trainer12Id, @Reception8Id, 'Trainer', 'olszewska@fitzone.pl', 'password123', 'Oliwia', 'Olszewska', '+48700200212'),

    -- Trenerzy AquaFit
    (@Trainer13Id, @Reception9Id, 'Trainer', 'adamczyk@aquafit.pl', 'password123', 'Piotr', 'Adamczyk', '+48700200213'),
    (@Trainer14Id, @Reception9Id, 'Trainer', 'rutkowska@aquafit.pl', 'password123', 'Renata', 'Rutkowska', '+48700200214'),
    (@Trainer15Id, @Reception10Id, 'Trainer', 'sikora@aquafit.pl', 'password123', 'Sebastian', 'Sikora', '+48700200215'),

    -- Trenerzy PowerGym
    (@Trainer16Id, @Reception11Id, 'Trainer', 'baran@powergym.pl', 'password123', 'Tomasz', 'Baran', '+48700200216'),
    (@Trainer17Id, @Reception11Id, 'Trainer', 'urbanska@powergym.pl', 'password123', 'Urszula', 'Urbańska', '+48700200217'),
    (@Trainer18Id, @Reception12Id, 'Trainer', 'walczak@powergym.pl', 'password123', 'Władysław', 'Walczak', '+48700200218'),

    -- Trenerzy FlexYoga
    (@Trainer19Id, @Reception13Id, 'Trainer', 'zakrzewski@flexyoga.pl', 'password123', 'Yolanda', 'Zakrzewska', '+48700200219'),
    (@Trainer20Id, @Reception13Id, 'Trainer', 'adamski@flexyoga.pl', 'password123', 'Zbigniew', 'Adamski', '+48700200220'),

    -- Managerowie
    (@Manager1Id, @Reception1Id, 'Manager', 'mazur.manager@sportfit.pl', 'password123', 'Janusz', 'Mazur', '+48700300301'),
    (@Manager2Id, @Reception2Id, 'Manager', 'kaczmarek@sportfit.pl', 'password123', 'Kamila', 'Kaczmarek', '+48700300302'),
    (@Manager3Id, @Reception3Id, 'Manager', 'grabowski@sportfit.pl', 'password123', 'Leszek', 'Grabowski', '+48700300303'),
    (@Manager4Id, @Reception4Id, 'Manager', 'kowalski@sportfit.pl', 'password123', 'Marcin', 'Kowalski', '+48700300304'),
    (@Manager5Id, @Reception5Id, 'Manager', 'nowakowska@sportfit.pl', 'password123', 'Nina', 'Nowakowska', '+48700300305'),
    (@Manager6Id, @Reception6Id, 'Manager', 'pawlowski@fitzone.pl', 'password123', 'Oscar', 'Pawłowski', '+48700300306'),
    (@Manager7Id, @Reception7Id, 'Manager', 'piotrowska@fitzone.pl', 'password123', 'Patrycja', 'Piotrowska', '+48700300307'),
    (@Manager8Id, @Reception9Id, 'Manager', 'rybak@aquafit.pl', 'password123', 'Robert', 'Rybak', '+48700300308'),
    (@Manager9Id, @Reception11Id, 'Manager', 'sokolowska@powergym.pl', 'password123', 'Sylwia', 'Sokołowska', '+48700300309'),
    (@Manager10Id, @Reception13Id, 'Manager', 'tomaszewski@flexyoga.pl', 'password123', 'Tomasz', 'Tomaszewski', '+48700300310');

-- =============================================
-- 5. Participants - dodanie uczestników (POPRAWIONE NUMERY)
-- =============================================

DECLARE @Participant1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant13Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant14Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant15Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant16Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant17Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant18Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant19Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant20Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant21Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant22Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant23Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant24Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant25Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant26Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant27Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant28Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant29Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Participant30Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO Participants (Id, ReceptionId, Email, FirstName, LastName, Phone, GdprConsent)
VALUES
    -- Uczestnicy SportFit Centrum
    (@Participant1Id, @Reception1Id, 'marek.adamski@email.com', 'Marek', 'Adamski', '+48800400401', 1),
    (@Participant2Id, @Reception1Id, 'natalia.barska@gmail.com', 'Natalia', 'Barska', '+48800400402', 1),
    (@Participant3Id, @Reception1Id, 'olgierd.cichocki@outlook.com', 'Olgierd', 'Cichocki', '+48800400403', 1),
    (@Participant4Id, @Reception1Id, 'anna.dabrowska@yahoo.com', 'Anna', 'Dąbrowska', '+48800400404', 1),

    -- Uczestnicy SportFit Południe
    (@Participant5Id, @Reception2Id, 'patrycja.dabrowska@email.com', 'Patrycja', 'Dąbrowska', '+48800400405', 1),
    (@Participant6Id, @Reception2Id, 'rafal.eski@gmail.com', 'Rafał', 'Eski', '+48800400406', 1),
    (@Participant7Id, @Reception2Id, 'sylwia.frankowska@outlook.com', 'Sylwia', 'Frankowska', '+48800400407', 1),
    (@Participant8Id, @Reception2Id, 'krzysztof.glowacki@yahoo.com', 'Krzysztof', 'Głowacki', '+48800400408', 1),

    -- Uczestnicy SportFit Północ
    (@Participant9Id, @Reception3Id, 'tomasz.gorski@email.com', 'Tomasz', 'Górski', '+48800400409', 1),
    (@Participant10Id, @Reception3Id, 'urszula.horak@gmail.com', 'Urszula', 'Horak', '+48800400410', 1),
    (@Participant11Id, @Reception3Id, 'wiktor.iwanski@outlook.com', 'Wiktor', 'Iwański', '+48800400411', 1),

    -- Uczestnicy SportFit Wschód
    (@Participant12Id, @Reception4Id, 'karolina.jaworska@email.com', 'Karolina', 'Jaworska', '+48800400412', 1),
    (@Participant13Id, @Reception4Id, 'lukasz.kowalski@gmail.com', 'Łukasz', 'Kowalski', '+48800400413', 1),

    -- Uczestnicy SportFit Zachód
    (@Participant14Id, @Reception5Id, 'magdalena.lewicz@outlook.com', 'Magdalena', 'Lewicz', '+48800400414', 1),
    (@Participant15Id, @Reception5Id, 'norbert.mazurek@yahoo.com', 'Norbert', 'Mazurek', '+48800400415', 1),

    -- Uczestnicy FitZone Mokotów
    (@Participant16Id, @Reception6Id, 'oliwia.nowak@email.com', 'Oliwia', 'Nowak', '+48800400416', 1),
    (@Participant17Id, @Reception6Id, 'pawel.osinski@gmail.com', 'Paweł', 'Osiński', '+48800400417', 1),
    (@Participant18Id, @Reception6Id, 'renata.piotrkowska@outlook.com', 'Renata', 'Piotrkowska', '+48800400418', 1),

    -- Uczestnicy FitZone Katowice
    (@Participant19Id, @Reception7Id, 'sebastian.rutkowski@email.com', 'Sebastian', 'Rutkowski', '+48800400419', 1),
    (@Participant20Id, @Reception7Id, 'tatiana.sikorska@gmail.com', 'Tatiana', 'Sikorska', '+48800400420', 1),

    -- Uczestnicy FitZone Poznań
    (@Participant21Id, @Reception8Id, 'urszula.tomczak@outlook.com', 'Urszula', 'Tomczak', '+48800400421', 1),
    (@Participant22Id, @Reception8Id, 'viktor.wisniewski@yahoo.com', 'Viktor', 'Wiśniewski', '+48800400422', 1),

    -- Uczestnicy AquaFit Marina
    (@Participant23Id, @Reception9Id, 'wanda.zalewski@email.com', 'Wanda', 'Zalewski', '+48800400423', 1),
    (@Participant24Id, @Reception9Id, 'xavier.adamczyk@gmail.com', 'Xavier', 'Adamczyk', '+48800400424', 1),

    -- Uczestnicy AquaFit Sopot
    (@Participant25Id, @Reception10Id, 'yvonne.bednarska@outlook.com', 'Yvonne', 'Bednarska', '+48800400425', 1),
    (@Participant26Id, @Reception10Id, 'zbigniew.czarnecki@yahoo.com', 'Zbigniew', 'Czarnecki', '+48800400426', 1),

    -- Uczestnicy PowerGym Center
    (@Participant27Id, @Reception11Id, 'agata.dabek@email.com', 'Agata', 'Dąbek', '+48800400427', 1),
    (@Participant28Id, @Reception11Id, 'bartosz.eliasz@gmail.com', 'Bartosz', 'Eliasz', '+48800400428', 1),

    -- Uczestnicy PowerGym Bydgoszcz
    (@Participant29Id, @Reception12Id, 'celina.filipiak@outlook.com', 'Celina', 'Filipiak', '+48800400429', 1),
    (@Participant30Id, @Reception12Id, 'damian.gorzynski@yahoo.com', 'Damian', 'Górzyński', '+48800400430', 1);

-- =============================================
-- 6. Specializations - dodanie specjalizacji
-- =============================================

DECLARE @Spec1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec13Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec14Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec15Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec16Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec17Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec18Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec19Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Spec20Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO Specializations (Id, ReceptionId, Name, Description)
VALUES
    -- SportFit Centrum
    (@Spec1Id, @Reception1Id, 'Joga', 'Zajęcia jogi dla początkujących i zaawansowanych'),
    (@Spec2Id, @Reception1Id, 'Trening siłowy', 'Zajęcia na siłowni z trenerem personalnym'),
    (@Spec3Id, @Reception1Id, 'Pilates', 'Ćwiczenia wzmacniające i rozciągające'),

    -- SportFit Południe
    (@Spec4Id, @Reception2Id, 'Crossfit', 'Intensywny trening crossfit dla wszystkich poziomów'),
    (@Spec5Id, @Reception2Id, 'Spinning', 'Zajęcia na rowerach stacjonarnych'),
    (@Spec6Id, @Reception2Id, 'Zumba', 'Energiczne zajęcia taneczne'),

    -- SportFit Północ
    (@Spec7Id, @Reception3Id, 'Pływanie', 'Nauka pływania i doskonalenie techniki'),
    (@Spec8Id, @Reception3Id, 'Aqua aerobik', 'Ćwiczenia w wodzie'),

    -- SportFit Wschód
    (@Spec9Id, @Reception4Id, 'Box', 'Trening bokserski dla różnych poziomów'),
    (@Spec10Id, @Reception4Id, 'Kickboxing', 'Sztuki walki z elementami cardio'),

    -- SportFit Zachód
    (@Spec11Id, @Reception5Id, 'TRX', 'Trening funkcjonalny z linami TRX'),
    (@Spec12Id, @Reception5Id, 'Stretching', 'Zajęcia rozciągające i relaksacyjne'),

    -- FitZone Mokotów
    (@Spec13Id, @Reception6Id, 'HIIT', 'Trening interwałowy wysokiej intensywności'),
    (@Spec14Id, @Reception6Id, 'Body Pump', 'Zajęcia z ciężarkami do muzyki'),

    -- FitZone Katowice
    (@Spec15Id, @Reception7Id, 'Tabata', 'Krótkie, intensywne treningi'),

    -- FitZone Poznań
    (@Spec16Id, @Reception8Id, 'Functional Training', 'Trening funkcjonalny całego ciała'),

    -- AquaFit Marina
    (@Spec17Id, @Reception9Id, 'Pływanie sportowe', 'Zaawansowane techniki pływackie'),

    -- AquaFit Sopot
    (@Spec18Id, @Reception10Id, 'Aqua fitness', 'Kompleksowy trening w wodzie'),

    -- PowerGym Center
    (@Spec19Id, @Reception11Id, 'Powerlifting', 'Trening siłowy - martwy ciąg, przysiad, wyciskanie'),

    -- FlexYoga Wilanów
    (@Spec20Id, @Reception13Id, 'Hatha Yoga', 'Klasyczna joga z naciskiem na pozycje');

-- =============================================
-- 7. StaffSpecializations - przypisanie specjalizacji do trenerów
-- =============================================

INSERT INTO StaffSpecializations (Id, ReceptionId, StaffId, SpecializationId)
VALUES
    -- SportFit Centrum
    (NEWID(), @Reception1Id, @Trainer1Id, @Spec1Id),
    (NEWID(), @Reception1Id, @Trainer1Id, @Spec2Id),
    (NEWID(), @Reception1Id, @Trainer2Id, @Spec2Id),
    (NEWID(), @Reception1Id, @Trainer2Id, @Spec3Id),

    -- SportFit Południe
    (NEWID(), @Reception2Id, @Trainer3Id, @Spec4Id),
    (NEWID(), @Reception2Id, @Trainer3Id, @Spec6Id),
    (NEWID(), @Reception2Id, @Trainer4Id, @Spec5Id),
    (NEWID(), @Reception2Id, @Trainer4Id, @Spec6Id),

    -- SportFit Północ
    (NEWID(), @Reception3Id, @Trainer5Id, @Spec7Id),
    (NEWID(), @Reception3Id, @Trainer5Id, @Spec8Id),
    (NEWID(), @Reception3Id, @Trainer6Id, @Spec7Id),

    -- SportFit Wschód
    (NEWID(), @Reception4Id, @Trainer7Id, @Spec9Id),
    (NEWID(), @Reception4Id, @Trainer7Id, @Spec10Id),

    -- SportFit Zachód
    (NEWID(), @Reception5Id, @Trainer8Id, @Spec11Id),
    (NEWID(), @Reception5Id, @Trainer8Id, @Spec12Id),

    -- FitZone Mokotów
    (NEWID(), @Reception6Id, @Trainer9Id, @Spec13Id),
    (NEWID(), @Reception6Id, @Trainer10Id, @Spec14Id),
    (NEWID(), @Reception6Id, @Trainer10Id, @Spec13Id),

    -- FitZone Katowice
    (NEWID(), @Reception7Id, @Trainer11Id, @Spec15Id),

    -- FitZone Poznań
    (NEWID(), @Reception8Id, @Trainer12Id, @Spec16Id),

    -- AquaFit Marina
    (NEWID(), @Reception9Id, @Trainer13Id, @Spec17Id),
    (NEWID(), @Reception9Id, @Trainer14Id, @Spec17Id),

    -- AquaFit Sopot
    (NEWID(), @Reception10Id, @Trainer15Id, @Spec18Id),

    -- PowerGym Center
    (NEWID(), @Reception11Id, @Trainer16Id, @Spec19Id),
    (NEWID(), @Reception11Id, @Trainer17Id, @Spec19Id),

    -- PowerGym Bydgoszcz
    (NEWID(), @Reception12Id, @Trainer18Id, @Spec19Id),

    -- FlexYoga Wilanów
    (NEWID(), @Reception13Id, @Trainer19Id, @Spec20Id),
    (NEWID(), @Reception13Id, @Trainer20Id, @Spec20Id);

-- =============================================
-- 8. StaffAvailability - dodanie dostępności personelu
-- =============================================

INSERT INTO StaffAvailability (Id, ReceptionId, StaffId, Date, StartTime, EndTime, IsAvailable)
VALUES
    -- Dostępność trenerów SportFit Centrum
    (NEWID(), @Reception1Id, @Trainer1Id, '2025-07-28', '2025-07-28 08:00:00', '2025-07-28 16:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer1Id, '2025-07-29', '2025-07-29 08:00:00', '2025-07-29 16:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer1Id, '2025-07-30', '2025-07-30 08:00:00', '2025-07-30 16:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer1Id, '2025-07-31', '2025-07-31 10:00:00', '2025-07-31 18:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer1Id, '2025-08-01', '2025-08-01 08:00:00', '2025-08-01 16:00:00', 1),

    (NEWID(), @Reception1Id, @Trainer2Id, '2025-07-28', '2025-07-28 10:00:00', '2025-07-28 18:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer2Id, '2025-07-29', '2025-07-29 10:00:00', '2025-07-29 18:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer2Id, '2025-07-30', '2025-07-30 10:00:00', '2025-07-30 18:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer2Id, '2025-07-31', '2025-07-31 12:00:00', '2025-07-31 20:00:00', 1),
    (NEWID(), @Reception1Id, @Trainer2Id, '2025-08-01', '2025-08-01 10:00:00', '2025-08-01 18:00:00', 1),

    -- Dostępność trenerów SportFit Południe
    (NEWID(), @Reception2Id, @Trainer3Id, '2025-07-28', '2025-07-28 09:00:00', '2025-07-28 17:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer3Id, '2025-07-29', '2025-07-29 09:00:00', '2025-07-29 17:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer3Id, '2025-07-30', '2025-07-30 09:00:00', '2025-07-30 17:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer3Id, '2025-07-31', '2025-07-31 11:00:00', '2025-07-31 19:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer3Id, '2025-08-01', '2025-08-01 09:00:00', '2025-08-01 17:00:00', 1),

    (NEWID(), @Reception2Id, @Trainer4Id, '2025-07-28', '2025-07-28 11:00:00', '2025-07-28 19:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer4Id, '2025-07-29', '2025-07-29 11:00:00', '2025-07-29 19:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer4Id, '2025-07-30', '2025-07-30 11:00:00', '2025-07-30 19:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer4Id, '2025-07-31', '2025-07-31 09:00:00', '2025-07-31 17:00:00', 1),
    (NEWID(), @Reception2Id, @Trainer4Id, '2025-08-01', '2025-08-01 11:00:00', '2025-08-01 19:00:00', 1),

    -- Dostępność trenerów SportFit Północ
    (NEWID(), @Reception3Id, @Trainer5Id, '2025-07-28', '2025-07-28 07:00:00', '2025-07-28 15:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer5Id, '2025-07-29', '2025-07-29 07:00:00', '2025-07-29 15:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer5Id, '2025-07-30', '2025-07-30 07:00:00', '2025-07-30 15:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer5Id, '2025-07-31', '2025-07-31 08:00:00', '2025-07-31 16:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer5Id, '2025-08-01', '2025-08-01 07:00:00', '2025-08-01 15:00:00', 1),

    (NEWID(), @Reception3Id, @Trainer6Id, '2025-07-28', '2025-07-28 13:00:00', '2025-07-28 21:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer6Id, '2025-07-29', '2025-07-29 13:00:00', '2025-07-29 21:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer6Id, '2025-07-30', '2025-07-30 13:00:00', '2025-07-30 21:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer6Id, '2025-07-31', '2025-07-31 15:00:00', '2025-07-31 21:00:00', 1),
    (NEWID(), @Reception3Id, @Trainer6Id, '2025-08-01', '2025-08-01 13:00:00', '2025-08-01 21:00:00', 1),

    -- Dostępność trenerów FitZone
    (NEWID(), @Reception6Id, @Trainer9Id, '2025-07-28', '2025-07-28 06:00:00', '2025-07-28 14:00:00', 1),
    (NEWID(), @Reception6Id, @Trainer9Id, '2025-07-29', '2025-07-29 06:00:00', '2025-07-29 14:00:00', 1),
    (NEWID(), @Reception6Id, @Trainer9Id, '2025-07-30', '2025-07-30 06:00:00', '2025-07-30 14:00:00', 1),

    (NEWID(), @Reception6Id, @Trainer10Id, '2025-07-28', '2025-07-28 14:00:00', '2025-07-28 22:00:00', 1),
    (NEWID(), @Reception6Id, @Trainer10Id, '2025-07-29', '2025-07-29 14:00:00', '2025-07-29 22:00:00', 1),
    (NEWID(), @Reception6Id, @Trainer10Id, '2025-07-30', '2025-07-30 14:00:00', '2025-07-30 22:00:00', 1),

    -- Dostępność trenerów AquaFit
    (NEWID(), @Reception9Id, @Trainer13Id, '2025-07-28', '2025-07-28 06:00:00', '2025-07-28 14:00:00', 1),
    (NEWID(), @Reception9Id, @Trainer13Id, '2025-07-29', '2025-07-29 06:00:00', '2025-07-29 14:00:00', 1),
    (NEWID(), @Reception9Id, @Trainer13Id, '2025-07-30', '2025-07-30 06:00:00', '2025-07-30 14:00:00', 1),

    (NEWID(), @Reception9Id, @Trainer14Id, '2025-07-28', '2025-07-28 14:00:00', '2025-07-28 22:00:00', 1),
    (NEWID(), @Reception9Id, @Trainer14Id, '2025-07-29', '2025-07-29 14:00:00', '2025-07-29 22:00:00', 1),
    (NEWID(), @Reception9Id, @Trainer14Id, '2025-07-30', '2025-07-30 14:00:00', '2025-07-30 22:00:00', 1),

    -- Dostępność trenerów PowerGym
    (NEWID(), @Reception11Id, @Trainer16Id, '2025-07-28', '2025-07-28 05:00:00', '2025-07-28 13:00:00', 1),
    (NEWID(), @Reception11Id, @Trainer16Id, '2025-07-29', '2025-07-29 05:00:00', '2025-07-29 13:00:00', 1),
    (NEWID(), @Reception11Id, @Trainer16Id, '2025-07-30', '2025-07-30 05:00:00', '2025-07-30 13:00:00', 1),

    (NEWID(), @Reception11Id, @Trainer17Id, '2025-07-28', '2025-07-28 13:00:00', '2025-07-28 21:00:00', 1),
    (NEWID(), @Reception11Id, @Trainer17Id, '2025-07-29', '2025-07-29 13:00:00', '2025-07-29 21:00:00', 1),
    (NEWID(), @Reception11Id, @Trainer17Id, '2025-07-30', '2025-07-30 13:00:00', '2025-07-30 21:00:00', 1),

    -- Dostępność trenerów FlexYoga
    (NEWID(), @Reception13Id, @Trainer19Id, '2025-07-28', '2025-07-28 08:00:00', '2025-07-28 16:00:00', 1),
    (NEWID(), @Reception13Id, @Trainer19Id, '2025-07-29', '2025-07-29 08:00:00', '2025-07-29 16:00:00', 1),
    (NEWID(), @Reception13Id, @Trainer19Id, '2025-07-30', '2025-07-30 08:00:00', '2025-07-30 16:00:00', 1),

    (NEWID(), @Reception13Id, @Trainer20Id, '2025-07-28', '2025-07-28 16:00:00', '2025-07-28 22:00:00', 1),
    (NEWID(), @Reception13Id, @Trainer20Id, '2025-07-29', '2025-07-29 16:00:00', '2025-07-29 22:00:00', 1),
    (NEWID(), @Reception13Id, @Trainer20Id, '2025-07-30', '2025-07-30 16:00:00', '2025-07-30 22:00:00', 1);

-- =============================================
-- 9. EventTypes - dodanie typów wydarzeń
-- =============================================

DECLARE @EventType1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType13Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType14Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType15Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType16Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType17Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType18Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType19Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType20Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType21Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType22Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType23Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType24Id UNIQUEIDENTIFIER = NEWID();
DECLARE @EventType25Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO EventTypes (Id, ReceptionId, Name, Description, Duration, Price, MaxParticipants, MinStaff)
VALUES
    -- SportFit Centrum
    (@EventType1Id, @Reception1Id, 'Joga dla początkujących', 'Zajęcia jogi dla osób początkujących', 60, 50.00, 15, 1),
    (@EventType2Id, @Reception1Id, 'Trening personalny', 'Indywidualne sesje z trenerem', 45, 120.00, 1, 1),
    (@EventType3Id, @Reception1Id, 'Pilates grupowy', 'Zajęcia pilates w małej grupie', 50, 45.00, 12, 1),

    -- SportFit Południe
    (@EventType4Id, @Reception2Id, 'Crossfit grupowy', 'Intensywny trening crossfit w grupie', 60, 60.00, 12, 1),
    (@EventType5Id, @Reception2Id, 'Spinning', 'Zajęcia na rowerach stacjonarnych', 45, 45.00, 20, 1),
    (@EventType6Id, @Reception2Id, 'Zumba party', 'Energiczne zajęcia taneczne', 55, 40.00, 25, 1),

    -- SportFit Północ
    (@EventType7Id, @Reception3Id, 'Nauka pływania', 'Lekcje pływania dla różnych poziomów', 60, 80.00, 8, 1),
    (@EventType8Id, @Reception3Id, 'Aqua aerobik', 'Ćwiczenia aerobowe w wodzie', 45, 55.00, 15, 1),

    -- SportFit Wschód
    (@EventType9Id, @Reception4Id, 'Trening bokserski', 'Podstawy boksu i trening kondycyjny', 60, 70.00, 10, 1),
    (@EventType10Id, @Reception4Id, 'Kickboxing', 'Sztuki walki z elementami cardio', 55, 65.00, 12, 1),

    -- SportFit Zachód
    (@EventType11Id, @Reception5Id, 'TRX Functional', 'Trening funkcjonalny z linami TRX', 50, 55.00, 14, 1),
    (@EventType12Id, @Reception5Id, 'Stretching & Relax', 'Zajęcia rozciągające i relaksacyjne', 45, 35.00, 20, 1),

    -- FitZone Mokotów
    (@EventType13Id, @Reception6Id, 'HIIT Power', 'Trening interwałowy wysokiej intensywności', 45, 60.00, 16, 1),
    (@EventType14Id, @Reception6Id, 'Body Pump', 'Zajęcia z ciężarkami do muzyki', 55, 50.00, 18, 1),

    -- FitZone Katowice
    (@EventType15Id, @Reception7Id, 'Tabata Express', 'Krótkie, intensywne treningi', 30, 40.00, 20, 1),

    -- FitZone Poznań
    (@EventType16Id, @Reception8Id, 'Functional Training', 'Trening funkcjonalny całego ciała', 60, 55.00, 14, 1),

    -- AquaFit Marina
    (@EventType17Id, @Reception9Id, 'Pływanie sportowe', 'Zaawansowane techniki pływackie', 60, 90.00, 6, 1),
    (@EventType18Id, @Reception9Id, 'Trening personalny pływanie', 'Indywidualne lekcje pływania', 45, 150.00, 1, 1),

    -- AquaFit Sopot
    (@EventType19Id, @Reception10Id, 'Aqua fitness', 'Kompleksowy trening w wodzie', 50, 60.00, 12, 1),
    (@EventType20Id, @Reception10Id, 'Aqua jogging', 'Bieganie w wodzie', 40, 45.00, 15, 1),

    -- PowerGym Center
    (@EventType21Id, @Reception11Id, 'Powerlifting', 'Trening siłowy - martwy ciąg, przysiad, wyciskanie', 90, 80.00, 8, 1),
    (@EventType22Id, @Reception11Id, 'Strongman training', 'Trening siłaczy', 75, 75.00, 10, 1),

    -- PowerGym Bydgoszcz
    (@EventType23Id, @Reception12Id, 'Bodybuilding', 'Trening na masę mięśniową', 75, 70.00, 12, 1),

    -- FlexYoga Wilanów
    (@EventType24Id, @Reception13Id, 'Hatha Yoga', 'Klasyczna joga z naciskiem na pozycje', 75, 60.00, 16, 1),
    (@EventType25Id, @Reception13Id, 'Yoga Nidra', 'Joga relaksacyjna i medytacyjna', 60, 55.00, 20, 1);

-- =============================================
-- 10. EventSchedules - dodanie harmonogramu wydarzeń
-- =============================================

DECLARE @Event1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event13Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event14Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event15Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event16Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event17Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event18Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event19Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event20Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event21Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event22Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event23Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event24Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event25Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event26Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event27Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event28Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event29Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event30Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event31Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event32Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event33Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event34Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Event35Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO EventSchedules (Id, ReceptionId, EventTypeId, PlaceName, StartTime, Status)
VALUES
    -- Wydarzenia SportFit Centrum
    (@Event1Id, @Reception1Id, @EventType1Id, 'Sala Fitness 1', '2025-07-28 10:00:00', 'Active'),
    (@Event2Id, @Reception1Id, @EventType2Id, 'Sala Treningowa 2', '2025-07-29 14:00:00', 'Active'),
    (@Event3Id, @Reception1Id, @EventType3Id, 'Sala Pilates', '2025-07-30 18:00:00', 'Active'),
    (@Event4Id, @Reception1Id, @EventType1Id, 'Sala Fitness 1', '2025-07-31 09:00:00', 'Active'),
    (@Event5Id, @Reception1Id, @EventType2Id, 'Sala Treningowa 2', '2025-08-01 15:00:00', 'Active'),

    -- Wydarzenia SportFit Południe
    (@Event6Id, @Reception2Id, @EventType4Id, 'Sala Crossfit', '2025-07-28 18:00:00', 'Active'),
    (@Event7Id, @Reception2Id, @EventType5Id, 'Sala Spinning', '2025-07-29 11:00:00', 'Active'),
    (@Event8Id, @Reception2Id, @EventType6Id, 'Sala Taneczna', '2025-07-30 19:00:00', 'Active'),
    (@Event9Id, @Reception2Id, @EventType4Id, 'Sala Crossfit', '2025-07-31 17:00:00', 'Active'),
    (@Event10Id, @Reception2Id, @EventType5Id, 'Sala Spinning', '2025-08-01 12:00:00', 'Active'),

    -- Wydarzenia SportFit Północ
    (@Event11Id, @Reception3Id, @EventType7Id, 'Basen - tor 1-2', '2025-07-28 09:00:00', 'Active'),
    (@Event12Id, @Reception3Id, @EventType8Id, 'Basen - cały', '2025-07-29 16:00:00', 'Active'),
    (@Event13Id, @Reception3Id, @EventType7Id, 'Basen - tor 3-4', '2025-07-30 10:00:00', 'Active'),
    (@Event14Id, @Reception3Id, @EventType8Id, 'Basen - cały', '2025-07-31 17:00:00', 'Active'),
    (@Event15Id, @Reception3Id, @EventType7Id, 'Basen - tor 1-2', '2025-08-01 08:00:00', 'Active'),

    -- Wydarzenia SportFit Wschód
    (@Event16Id, @Reception4Id, @EventType9Id, 'Sala Bokserska', '2025-07-28 18:00:00', 'Active'),
    (@Event17Id, @Reception4Id, @EventType10Id, 'Sala Kickboxing', '2025-07-29 19:00:00', 'Active'),
    (@Event18Id, @Reception4Id, @EventType9Id, 'Sala Bokserska', '2025-07-30 17:00:00', 'Active'),

    -- Wydarzenia SportFit Zachód
    (@Event19Id, @Reception5Id, @EventType11Id, 'Sala TRX', '2025-07-28 17:00:00', 'Active'),
    (@Event20Id, @Reception5Id, @EventType12Id, 'Sala Relaks', '2025-07-29 20:00:00', 'Active'),
    (@Event21Id, @Reception5Id, @EventType11Id, 'Sala TRX', '2025-07-30 16:00:00', 'Active'),

    -- Wydarzenia FitZone Mokotów
    (@Event22Id, @Reception6Id, @EventType13Id, 'Sala HIIT', '2025-07-28 07:00:00', 'Active'),
    (@Event23Id, @Reception6Id, @EventType14Id, 'Sala Body Pump', '2025-07-29 19:00:00', 'Active'),
    (@Event24Id, @Reception6Id, @EventType13Id, 'Sala HIIT', '2025-07-30 08:00:00', 'Active'),

    -- Wydarzenia FitZone Katowice
    (@Event25Id, @Reception7Id, @EventType15Id, 'Sala Express', '2025-07-28 12:00:00', 'Active'),
    (@Event26Id, @Reception7Id, @EventType15Id, 'Sala Express', '2025-07-29 18:00:00', 'Active'),

    -- Wydarzenia FitZone Poznań
    (@Event27Id, @Reception8Id, @EventType16Id, 'Sala Funkcjonalna', '2025-07-28 16:00:00', 'Active'),
    (@Event28Id, @Reception8Id, @EventType16Id, 'Sala Funkcjonalna', '2025-07-30 17:00:00', 'Active'),

    -- Wydarzenia AquaFit Marina
    (@Event29Id, @Reception9Id, @EventType17Id, 'Basen olimpijski - tor 1-3', '2025-07-28 07:00:00', 'Active'),
    (@Event30Id, @Reception9Id, @EventType18Id, 'Basen olimpijski - tor 4', '2025-07-29 15:00:00', 'Active'),
    (@Event31Id, @Reception9Id, @EventType17Id, 'Basen olimpijski - tor 1-3', '2025-07-30 08:00:00', 'Active'),

    -- Wydarzenia AquaFit Sopot
    (@Event32Id, @Reception10Id, @EventType19Id, 'Basen rekreacyjny', '2025-07-28 16:00:00', 'Active'),
    (@Event33Id, @Reception10Id, @EventType20Id, 'Basen sportowy', '2025-07-29 17:00:00', 'Active'),

    -- Wydarzenia PowerGym Center
    (@Event34Id, @Reception11Id, @EventType21Id, 'Sala Powerlifting', '2025-07-28 06:00:00', 'Active'),
    (@Event35Id, @Reception11Id, @EventType22Id, 'Sala Strongman', '2025-07-29 20:00:00', 'Active');

-- =============================================
-- 11. EventScheduleStaff - przypisanie personelu do wydarzeń
-- =============================================

INSERT INTO EventScheduleStaff (Id, ReceptionId, EventScheduleId, StaffId)
VALUES
    -- Przypisanie trenerów SportFit Centrum
    (NEWID(), @Reception1Id, @Event1Id, @Trainer1Id),
    (NEWID(), @Reception1Id, @Event2Id, @Trainer2Id),
    (NEWID(), @Reception1Id, @Event3Id, @Trainer2Id),
    (NEWID(), @Reception1Id, @Event4Id, @Trainer1Id),
    (NEWID(), @Reception1Id, @Event5Id, @Trainer2Id),

    -- Przypisanie trenerów SportFit Południe
    (NEWID(), @Reception2Id, @Event6Id, @Trainer3Id),
    (NEWID(), @Reception2Id, @Event7Id, @Trainer4Id),
    (NEWID(), @Reception2Id, @Event8Id, @Trainer3Id),
    (NEWID(), @Reception2Id, @Event9Id, @Trainer3Id),
    (NEWID(), @Reception2Id, @Event10Id, @Trainer4Id),

    -- Przypisanie trenerów SportFit Północ
    (NEWID(), @Reception3Id, @Event11Id, @Trainer5Id),
    (NEWID(), @Reception3Id, @Event12Id, @Trainer5Id),
    (NEWID(), @Reception3Id, @Event13Id, @Trainer6Id),
    (NEWID(), @Reception3Id, @Event14Id, @Trainer5Id),
    (NEWID(), @Reception3Id, @Event15Id, @Trainer6Id),

    -- Przypisanie trenerów SportFit Wschód
    (NEWID(), @Reception4Id, @Event16Id, @Trainer7Id),
    (NEWID(), @Reception4Id, @Event17Id, @Trainer7Id),
    (NEWID(), @Reception4Id, @Event18Id, @Trainer7Id),

    -- Przypisanie trenerów SportFit Zachód
    (NEWID(), @Reception5Id, @Event19Id, @Trainer8Id),
    (NEWID(), @Reception5Id, @Event20Id, @Trainer8Id),
    (NEWID(), @Reception5Id, @Event21Id, @Trainer8Id),

    -- Przypisanie trenerów FitZone
    (NEWID(), @Reception6Id, @Event22Id, @Trainer9Id),
    (NEWID(), @Reception6Id, @Event23Id, @Trainer10Id),
    (NEWID(), @Reception6Id, @Event24Id, @Trainer9Id),
    (NEWID(), @Reception7Id, @Event25Id, @Trainer11Id),
    (NEWID(), @Reception7Id, @Event26Id, @Trainer11Id),
    (NEWID(), @Reception8Id, @Event27Id, @Trainer12Id),
    (NEWID(), @Reception8Id, @Event28Id, @Trainer12Id),

    -- Przypisanie trenerów AquaFit
    (NEWID(), @Reception9Id, @Event29Id, @Trainer13Id),
    (NEWID(), @Reception9Id, @Event30Id, @Trainer14Id),
    (NEWID(), @Reception9Id, @Event31Id, @Trainer13Id),
    (NEWID(), @Reception10Id, @Event32Id, @Trainer15Id),
    (NEWID(), @Reception10Id, @Event33Id, @Trainer15Id),

    -- Przypisanie trenerów PowerGym
    (NEWID(), @Reception11Id, @Event34Id, @Trainer16Id),
    (NEWID(), @Reception11Id, @Event35Id, @Trainer17Id);

-- =============================================
-- 12. Reservations - dodanie rezerwacji
-- =============================================

DECLARE @Reservation1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation5Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation6Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation7Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation8Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation9Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation10Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation11Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation12Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation13Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation14Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation15Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation16Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation17Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation18Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation19Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation20Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation21Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation22Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation23Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation24Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Reservation25Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO Reservations (Id, ReceptionId, ParticipantId, EventScheduleId, ParticipantCount, Status, Notes, CreatedAt, CancelledAt, PaidAt)
VALUES
    -- Rezerwacje SportFit Centrum
    (@Reservation1Id, @Reception1Id, @Participant1Id, @Event1Id, 1, 'Confirmed', 'Pierwszy raz na zajęciach jogi', '2025-07-25 14:00:00', NULL, '2025-07-25 14:30:00'),
    (@Reservation2Id, @Reception1Id, @Participant2Id, @Event2Id, 1, 'Confirmed', 'Trening personalny - cel: budowa masy mięśniowej', '2025-07-26 08:45:00', NULL, '2025-07-26 09:15:00'),
    (@Reservation3Id, @Reception1Id, @Participant3Id, @Event3Id, 1, 'Confirmed', 'Zajęcia pilates - problemy z kręgosłupem', '2025-07-27 16:15:00', NULL, '2025-07-27 16:45:00'),
    (@Reservation4Id, @Reception1Id, @Participant4Id, @Event4Id, 1, 'Confirmed', 'Kontynuacja zajęć jogi', '2025-07-28 07:50:00', NULL, '2025-07-28 08:20:00'),

    -- Rezerwacje SportFit Południe
    (@Reservation5Id, @Reception2Id, @Participant5Id, @Event6Id, 1, 'Confirmed', 'Doświadczony w crossfit', '2025-07-25 11:15:00', NULL, '2025-07-25 11:45:00'),
    (@Reservation6Id, @Reception2Id, @Participant6Id, @Event7Id, 1, 'Confirmed', 'Lubię spinning - świetna muzyka', '2025-07-26 13:00:00', NULL, '2025-07-26 13:30:00'),
    (@Reservation7Id, @Reception2Id, @Participant7Id, @Event8Id, 1, 'Confirmed', 'Pierwszy raz na zumbie', '2025-07-27 17:45:00', NULL, '2025-07-27 18:15:00'),
    (@Reservation8Id, @Reception2Id, @Participant8Id, @Event9Id, 1, 'Confirmed', 'Oczekiwanie na potwierdzenie', '2025-07-28 09:30:00', NULL, NULL),

    -- Rezerwacje SportFit Północ
    (@Reservation9Id, @Reception3Id, @Participant9Id, @Event11Id, 1, 'Confirmed', 'Początkujący - nauka podstaw pływania', '2025-07-25 15:50:00', NULL, '2025-07-25 16:20:00'),
    (@Reservation10Id, @Reception3Id, @Participant10Id, @Event12Id, 1, 'Confirmed', 'Aqua aerobik - rehabilitacja po kontuzji', '2025-07-26 10:15:00', NULL, '2025-07-26 10:45:00'),
    (@Reservation11Id, @Reception3Id, @Participant11Id, @Event13Id, 1, 'Cancelled', 'Zmiana planów - choroba', '2025-07-27 14:30:00', '2025-07-27 15:45:00', NULL),
    (@Reservation12Id, @Reception3Id, @Participant9Id, @Event15Id, 1, 'Confirmed', 'Kontynuacja nauki pływania', '2025-07-28 07:00:00', NULL, '2025-07-28 07:30:00'),

    -- Rezerwacje SportFit Wschód
    (@Reservation13Id, @Reception4Id, @Participant12Id, @Event16Id, 1, 'Confirmed', 'Chcę nauczyć się boksu', '2025-07-26 15:15:00', NULL, '2025-07-26 15:45:00'),
    (@Reservation14Id, @Reception4Id, @Participant13Id, @Event17Id, 1, 'Confirmed', 'Kickboxing na spalanie kalorii', '2025-07-27 16:50:00', NULL, '2025-07-27 17:20:00'),

    -- Rezerwacje SportFit Zachód
    (@Reservation15Id, @Reception5Id, @Participant14Id, @Event19Id, 1, 'Confirmed', 'TRX - trening funkcjonalny', '2025-07-26 13:40:00', NULL, '2025-07-26 14:10:00'),
    (@Reservation16Id, @Reception5Id, @Participant15Id, @Event20Id, 1, 'Confirmed', 'Potrzebuję relaksu po pracy', '2025-07-27 19:00:00', NULL, '2025-07-27 19:30:00'),

    -- Rezerwacje FitZone Mokotów
    (@Reservation17Id, @Reception6Id, @Participant16Id, @Event22Id, 1, 'Confirmed', 'HIIT - chcę szybko spalić kalorie', '2025-07-25 19:45:00', NULL, '2025-07-25 20:15:00'),
    (@Reservation18Id, @Reception6Id, @Participant17Id, @Event23Id, 1, 'Confirmed', 'Body Pump - budowanie siły', '2025-07-27 16:15:00', NULL, '2025-07-27 16:45:00'),
    (@Reservation19Id, @Reception6Id, @Participant18Id, @Event24Id, 1, 'Confirmed', 'Kolejne HIIT - jestem uzależniona!', '2025-07-28 06:00:00', NULL, '2025-07-28 06:30:00'),

    -- Rezerwacje FitZone Katowice
    (@Reservation20Id, @Reception7Id, @Participant19Id, @Event25Id, 1, 'Confirmed', 'Tabata - krótko i intensywnie', '2025-07-26 10:50:00', NULL, '2025-07-26 11:20:00'),
    (@Reservation21Id, @Reception7Id, @Participant20Id, @Event26Id, 1, 'Confirmed', 'Wieczorna sesja Tabata', '2025-07-27 17:10:00', NULL, '2025-07-27 17:40:00'),

    -- Rezerwacje AquaFit Marina
    (@Reservation22Id, @Reception9Id, @Participant23Id, @Event29Id, 1, 'Confirmed', 'Pływanie sportowe - przygotowanie do zawodów', '2025-07-26 18:00:00', NULL, '2025-07-26 18:30:00'),
    (@Reservation23Id, @Reception9Id, @Participant24Id, @Event30Id, 1, 'Confirmed', 'Trening personalny - technika kraul', '2025-07-27 11:45:00', NULL, '2025-07-27 12:15:00'),

    -- Rezerwacje PowerGym Center
    (@Reservation24Id, @Reception11Id, @Participant27Id, @Event34Id, 1, 'Confirmed', 'Powerlifting - chcę bić rekordy', '2025-07-26 05:15:00', NULL, '2025-07-26 05:45:00'),
    (@Reservation25Id, @Reception11Id, @Participant28Id, @Event35Id, 1, 'Confirmed', 'Strongman - siłacz w sobie', '2025-07-27 19:20:00', NULL, '2025-07-27 19:50:00');

-- =============================================
-- 13. Notifications - dodanie powiadomień
-- =============================================

INSERT INTO Notifications (Id, ReceptionId, ReservationId, EmailStatus, SmsStatus, EmailSentAt, SmsSentAt, EmailContent, SmsContent)
VALUES
    -- Powiadomienia SportFit Centrum
    (NEWID(), @Reception1Id, @Reservation1Id, 'Sent', 'Sent', '2025-07-25 14:35:00', '2025-07-25 14:35:00',
     'Potwierdzenie rezerwacji na zajęcia jogi dla początkujących w dniu 28.07.2025 o godz. 10:00. Dziękujemy za wybór SportFit!',
     'SportFit: Joga 28.07.2025, 10:00 - Sala Fitness 1'),

    (NEWID(), @Reception1Id, @Reservation2Id, 'Sent', 'Sent', '2025-07-26 09:20:00', '2025-07-26 09:20:00',
     'Potwierdzenie rezerwacji na trening personalny w dniu 29.07.2025 o godz. 14:00. Trener: Ewa Jabłońska.',
     'SportFit: Trening personalny 29.07.2025, 14:00 - Ewa Jabłońska'),

    (NEWID(), @Reception1Id, @Reservation3Id, 'Sent', 'Sent', '2025-07-27 16:50:00', '2025-07-27 16:50:00',
     'Potwierdzenie rezerwacji na pilates grupowy w dniu 30.07.2025 o godz. 18:00. Sala Pilates.',
     'SportFit: Pilates 30.07.2025, 18:00 - Sala Pilates'),

    -- Powiadomienia SportFit Południe
    (NEWID(), @Reception2Id, @Reservation5Id, 'Sent', 'Sent', '2025-07-25 11:50:00', '2025-07-25 11:50:00',
     'Potwierdzenie rezerwacji na crossfit grupowy w dniu 28.07.2025 o godz. 18:00. Przygotuj się na intensywny trening!',
     'SportFit: Crossfit 28.07.2025, 18:00 - Sala Crossfit'),

    (NEWID(), @Reception2Id, @Reservation6Id, 'Sent', 'Sent', '2025-07-26 13:35:00', '2025-07-26 13:35:00',
     'Potwierdzenie rezerwacji na spinning w dniu 29.07.2025 o godz. 11:00. Przynieś ręcznik i butelkę wody.',
     'SportFit: Spinning 29.07.2025, 11:00 - Sala Spinning'),

    (NEWID(), @Reception2Id, @Reservation7Id, 'Sent', 'Sent', '2025-07-27 18:20:00', '2025-07-27 18:20:00',
     'Potwierdzenie rezerwacji na zumba party w dniu 30.07.2025 o godz. 19:00. Tańcz i baw się!',
     'SportFit: Zumba 30.07.2025, 19:00 - Sala Taneczna'),

    -- Powiadomienia SportFit Północ
    (NEWID(), @Reception3Id, @Reservation9Id, 'Sent', 'Sent', '2025-07-25 16:25:00', '2025-07-25 16:25:00',
     'Potwierdzenie rezerwacji na naukę pływania w dniu 28.07.2025 o godz. 09:00. Basen - tor 1-2.',
     'SportFit: Pływanie 28.07.2025, 09:00 - Basen tor 1-2'),

    (NEWID(), @Reception3Id, @Reservation10Id, 'Sent', 'Sent', '2025-07-26 10:50:00', '2025-07-26 10:50:00',
     'Potwierdzenie rezerwacji na aqua aerobik w dniu 29.07.2025 o godz. 16:00. Trener: Henryk Zieliński.',
     'SportFit: Aqua aerobik 29.07.2025, 16:00 - Henryk Zieliński'),

    (NEWID(), @Reception3Id, @Reservation11Id, 'Sent', 'Sent', '2025-07-28 09:00:00', '2025-07-28 09:00:00',
     'Potwierdzenie anulowania rezerwacji na naukę pływania w dniu 30.07.2025 o godz. 10:00. Życzymy szybkiego powrotu do zdrowia!',
     'SportFit: Anulowanie - Pływanie 30.07.2025, 10:00'),

    -- Powiadomienia FitZone
    (NEWID(), @Reception6Id, @Reservation17Id, 'Sent', 'Sent', '2025-07-25 20:20:00', '2025-07-25 20:20:00',
     'Potwierdzenie rezerwacji na HIIT Power w dniu 28.07.2025 o godz. 07:00. FitZone Mokotów - Sala HIIT.',
     'FitZone: HIIT 28.07.2025, 07:00 - Sala HIIT'),

    (NEWID(), @Reception6Id, @Reservation18Id, 'Sent', 'Sent', '2025-07-27 16:50:00', '2025-07-27 16:50:00',
     'Potwierdzenie rezerwacji na Body Pump w dniu 29.07.2025 o godz. 19:00. Trener: Marta Pawlak.',
     'FitZone: Body Pump 29.07.2025, 19:00 - Marta Pawlak'),

    -- Powiadomienia AquaFit
    (NEWID(), @Reception9Id, @Reservation22Id, 'Sent', 'Sent', '2025-07-26 18:35:00', '2025-07-26 18:35:00',
     'Potwierdzenie rezerwacji na pływanie sportowe w dniu 28.07.2025 o godz. 07:00. AquaFit Marina - tor 1-3.',
     'AquaFit: Pływanie sportowe 28.07.2025, 07:00'),

    (NEWID(), @Reception9Id, @Reservation23Id, 'Sent', 'Sent', '2025-07-27 12:20:00', '2025-07-27 12:20:00',
     'Potwierdzenie rezerwacji na trening personalny pływanie w dniu 29.07.2025 o godz. 15:00. Trener: Renata Rutkowska.',
     'AquaFit: Trening personalny 29.07.2025, 15:00'),

    -- Powiadomienia PowerGym
    (NEWID(), @Reception11Id, @Reservation24Id, 'Sent', 'Sent', '2025-07-26 05:50:00', '2025-07-26 05:50:00',
     'Potwierdzenie rezerwacji na powerlifting w dniu 28.07.2025 o godz. 06:00. PowerGym Center - Sala Powerlifting.',
     'PowerGym: Powerlifting 28.07.2025, 06:00'),

    (NEWID(), @Reception11Id, @Reservation25Id, 'Sent', 'Sent', '2025-07-27 19:55:00', '2025-07-27 19:55:00',
     'Potwierdzenie rezerwacji na strongman training w dniu 29.07.2025 o godz. 20:00. Trener: Urszula Urbańska.',
     'PowerGym: Strongman 29.07.2025, 20:00 - Urszula Urbańska');

-- =============================================
-- 14. Messages - dodanie wiadomości
-- =============================================

INSERT INTO Messages (Id, ReceptionId, SenderId, ReceiverId, Content)
VALUES
    -- Wiadomości SportFit
    (NEWID(), @Reception1Id, @Manager1Id, @Trainer1Id, 'Proszę o przygotowanie planu zajęć jogi na sierpień. Zwiększone zainteresowanie!'),
    (NEWID(), @Reception1Id, @Trainer1Id, @Manager1Id, 'Plan zajęć jogi na sierpień będzie gotowy do piątku. Dodaję 2 dodatkowe grupy.'),
    (NEWID(), @Reception1Id, @StaffRec1Id, @Manager1Id, 'Recepcja raportuje: 15 nowych członków w tym tygodniu!'),
    (NEWID(), @Reception1Id, @Manager1Id, @Trainer2Id, 'Świetna opinia o Twoich treningach personalnych. Kontynuuj dobrą pracę!'),
    (NEWID(), @Reception1Id, @Trainer2Id, @Manager1Id, 'Dziękuję za uznanie. Czy mogę poprowadzić warsztaty pilates w weekend?'),

    (NEWID(), @Reception2Id, @Manager2Id, @Trainer3Id, 'Czy możesz poprowadzić dodatkowe zajęcia crossfit w sobotę?'),
    (NEWID(), @Reception2Id, @Trainer3Id, @Manager2Id, 'Tak, mogę poprowadzić dodatkowe zajęcia crossfit w sobotę o 16:00'),
    (NEWID(), @Reception2Id, @StaffRec2Id, @Manager2Id, 'Sala spinning wymaga serwisu rowerów - zgłoszenia od uczestników'),
    (NEWID(), @Reception2Id, @Manager2Id, @StaffRec2Id, 'Dzięki za info. Zamawiam serwis na jutro rano przed zajęciami.'),

    (NEWID(), @Reception3Id, @StaffRec3Id, @Manager3Id, 'Komplet zapisów na zajęcia pływania w przyszłym tygodniu. Rozważamy dodatkową grupę?'),
    (NEWID(), @Reception3Id, @Manager3Id, @Trainer5Id, 'Henryk, czy mógłbyś poprowadzić dodatkową grupę pływania w czwartek?'),
    (NEWID(), @Reception3Id, @Trainer5Id, @Manager3Id, 'Oczywiście! Czwartek 18:00 będzie idealny dla dodatkowej grupy.'),

    -- Wiadomości FitZone
    (NEWID(), @Reception6Id, @Manager6Id, @Trainer9Id, 'Świetny feedback na zajęcia HIIT! Rozważamy zwiększenie częstotliwości.'),
    (NEWID(), @Reception6Id, @Trainer9Id, @Manager6Id, 'Cieszę się! Mogę dodać sesje HIIT w środy i piątki.'),
    (NEWID(), @Reception6Id, @StaffRec6Id, @Manager6Id, 'Nowy członek pyta o zajęcia dla seniorów. Czy planujemy taką grupę?'),
    (NEWID(), @Reception6Id, @Manager6Id, @StaffRec6Id, 'Dobry pomysł! Porozmawiam z trenerami o programie 50+'),

    (NEWID(), @Reception7Id, @Manager7Id, @Trainer11Id, 'Tabata cieszy się ogromną popularnością! Brawo!'),
    (NEWID(), @Reception7Id, @Trainer11Id, @Manager7Id, 'Dziękuję! Uczestnicy są bardzo zmotywowani. Może warsztaty weekendowe?'),

    -- Wiadomości AquaFit
    (NEWID(), @Reception9Id, @Manager8Id, @Trainer13Id, 'Zawodnik z naszych zajęć wygrał regionalne zawody! Gratulacje!'),
    (NEWID(), @Reception9Id, @Trainer13Id, @Manager8Id, 'To wspaniała wiadomość! Ciężka praca się opłaciła.'),
    (NEWID(), @Reception9Id, @StaffRec9Id, @Manager8Id, 'Zapytania o obozy pływackie na wakacje. Organizujemy?'),
    (NEWID(), @Reception9Id, @Manager8Id, @Trainer14Id, 'Renata, czy chciałabyś współorganizować obóz pływacki?'),
    (NEWID(), @Reception9Id, @Trainer14Id, @Manager8Id, 'Z przyjemnością! Mam już pomysły na program.'),

    -- Wiadomości PowerGym
    (NEWID(), @Reception11Id, @Manager9Id, @Trainer16Id, 'Tomasz, świetne wyniki uczestników w powerlifting!'),
    (NEWID(), @Reception11Id, @Trainer16Id, @Manager9Id, 'Dziękuję! Planujemy udział w zawodach wojewódzkich.'),
    (NEWID(), @Reception11Id, @StaffRec11Id, @Manager9Id, 'Prośba o dodanie zajęć dla kobiet zainteresowanych siłownią'),
    (NEWID(), @Reception11Id, @Manager9Id, @Trainer17Id, 'Urszula, czy poprowadzisz grupę "Ladies Power"?'),
    (NEWID(), @Reception11Id, @Trainer17Id, @Manager9Id, 'Świetny pomysł! Kobiety potrzebują dedykowanych zajęć.'),

    -- Wiadomości FlexYoga
    (NEWID(), @Reception13Id, @Manager10Id, @Trainer19Id, 'Meditation workshop otrzymał fantastyczne recenzje!'),
    (NEWID(), @Reception13Id, @Trainer19Id, @Manager10Id, 'Dziękuję! Planujemy cykl warsztatów mindfulness.'),
    (NEWID(), @Reception13Id, @Trainer20Id, @Manager10Id, 'Zbigniew, czy mógłbyś poprowadzić zajęcia jogi dla par?'),
    (NEWID(), @Reception13Id, @Manager10Id, @Trainer20Id, 'Interesujący pomysł na walentynki! Przygotujemy program.');

-- =============================================
-- KONIEC SKRYPTU
-- =============================================

PRINT 'Dane przykładowe zostały pomyślnie wstawione do bazy danych!'
PRINT 'Utworzono:'
PRINT '- 18 firm (Companies)'
PRINT '- 13 recepcji (Receptions)'
PRINT '- 43 pracowników (Staff)'
PRINT '- 30 uczestników (Participants)'
PRINT '- 20 specjalizacji (Specializations)'
PRINT '- 25 typów wydarzeń (EventTypes)'
PRINT '- 35 zaplanowanych wydarzeń (EventSchedules)'
PRINT '- 25 rezerwacji (Reservations)'
PRINT '- 15 powiadomień (Notifications)'
PRINT '- 32 wiadomości (Messages)'
PRINT ''
PRINT 'Wszystkie numery telefonów są unikalne!'
PRINT 'Skrypt zakończony pomyślnie.'