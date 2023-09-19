USE master
GO

--drop database if it exists
IF DB_ID('vpat') IS NOT NULL
BEGIN
	ALTER DATABASE vpat SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE vpat;
END

CREATE DATABASE vpat
GO

USE vpat
GO

--create tables
CREATE TABLE users (
	user_id INT IDENTITY(1,1) NOT NULL,
	username VARCHAR(50) NOT NULL,
    email VARCHAR(75) NOT NULL,
	password_hash VARCHAR(200) NOT NULL,
	salt VARCHAR(200) NOT NULL,
	user_role VARCHAR(50) NOT NULL,
    is_hidden BIT NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

CREATE TABLE pets (
    pet_id INT IDENTITY(1,1) NOT NULL,
    pet_name NVARCHAR(50) NOT NULL,
    pet_type NVARCHAR(50) NULL,
    brand NVARCHAR(50) NOT NULL,
    date_birth VARCHAR(10) NOT NULL,
    time_birth VARCHAR(12) NULL,
    is_active BIT NOT NULL,
    is_hidden BIT NOT NULL,
    CONSTRAINT PK_pet PRIMARY KEY (pet_id)
);

CREATE TABLE users_pets (
    user_id INT NOT NULL,
    pet_id INT NOT NULL,
    CONSTRAINT PK_users_pets PRIMARY KEY (user_id, pet_id),
    CONSTRAINT FK_users_pets_user FOREIGN KEY (user_id) REFERENCES [users] (user_id),
    CONSTRAINT FK_users_pets_pet FOREIGN KEY (pet_id) REFERENCES [pets] (pet_id)
);

ALTER TABLE users ADD CONSTRAINT UQ_UserName UNIQUE(username);
ALTER TABLE users ADD CONSTRAINT UQ_Email UNIQUE(email);

--populate default data
INSERT INTO users (username, email, password_hash, salt, user_role, is_hidden) VALUES ('jemster','emelie.kerek@gmail.com','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','admin',0);
INSERT INTO users (username, email, password_hash, salt, user_role, is_hidden) VALUES ('user','test.user@email.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user',0);
INSERT INTO users (username, email, password_hash, salt, user_role, is_hidden) VALUES ('tester','big.fan@email.com','OXKMP3D9m6KRboiHQPTuI3HFCGU=', 'N2w2Mti3pYM=','user',1);

GO