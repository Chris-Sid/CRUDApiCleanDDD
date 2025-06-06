CREATE TABLE IF NOT EXISTS Customers (
    Id UUID PRIMARY KEY,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL,
    Phone TEXT,
    Status TEXT,
    CreatedDate TIMESTAMP NOT NULL,
    Tags JSONB,
    Contacts JSONB
);

CREATE TABLE IF NOT EXISTS Admins (
    Id UUID PRIMARY KEY,
    Username TEXT NOT NULL,
    PasswordHash TEXT,
    Role TEXT
);

CREATE TABLE IF NOT EXISTS Leads (
    Id UUID PRIMARY KEY,
    Name TEXT,
    Email TEXT,
    Status TEXT,
    CreatedDate TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Opportunities (
    Id UUID PRIMARY KEY,
    CustomerId UUID,
    Description TEXT,
    EstimatedValue DECIMAL,
    Status TEXT,
    CreatedDate TIMESTAMP,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE IF NOT EXISTS Notes (
    Id UUID PRIMARY KEY,
    CustomerId UUID,
    Content TEXT,
    CreatedDate TIMESTAMP,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE IF NOT EXISTS CommunicationLogs (
    Id UUID PRIMARY KEY,
    CustomerId UUID,
    Type TEXT,
    Content TEXT,
    CreatedDate TIMESTAMP,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

INSERT INTO Admins (Id, Username, PasswordHash, Role)
VALUES (
    gen_random_uuid(), 
    'admin', 
    '$argon2id$v=19$m=65536,t=3,p=1$9w0w8ZOHw1Gb0cMfF7uRKw$OZREtw1rTckSH/V2b8mrzCvvVZK+M5Sg6kUqUdCB0h4', 
    'Admin'
);