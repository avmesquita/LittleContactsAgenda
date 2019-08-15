BEGIN TRANSACTION;
CREATE TABLE "Telephone" (
	`TelephoneId`	TEXT,
	`ContactId`	TEXT NOT NULL,
	`TelephoneContent`	TEXT NOT NULL,
	`ContactType`	INTEGER,
	PRIMARY KEY(`TelephoneId`),
	FOREIGN KEY(`ContactId`) REFERENCES Contact(ContactId)
);
CREATE TABLE "Email" (
	`EmailId`	TEXT,
	`ContactId`	TEXT NOT NULL,
	`EmailContent`	TEXT NOT NULL,
	`ContactType`	INTEGER,
	PRIMARY KEY(`EmailId`),
	FOREIGN KEY(`ContactId`) REFERENCES Contact(ContactId)
);
CREATE TABLE "Contact" (
	`ContactId`	TEXT,
	`Name`	TEXT NOT NULL,
	`Observation`	TEXT,
	`FavoriteTag`	INTEGER DEFAULT 0,
	PRIMARY KEY(`ContactId`)
);
CREATE TABLE "Address" (
	`AddressId`	TEXT,
	`ContactId`	TEXT NOT NULL,
	`AddressContent`	TEXT NOT NULL,
	`ContactType`	INTEGER,
	PRIMARY KEY(`AddressId`),
	FOREIGN KEY(`ContactId`) REFERENCES Contact(ContactId)
);
COMMIT;
