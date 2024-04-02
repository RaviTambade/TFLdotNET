-- Active: 1676969830187@@127.0.0.1@3306@eagroservicesdb

Drop DATABASE IF EXISTS agrodb;
CREATE DATABASE agrodb;
USE agrodb;

CREATE TABLE
    roles(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name varchar(20)
    );

CREATE TABLE
    userroles(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        userid INT NOT NULL ,
        roleid INT NOT NULL,
        CONSTRAINT uc_userroles UNIQUE (userid, roleid),
        CONSTRAINT fk_userroles FOREIGN KEY(roleid) REFERENCES roles(id) ON UPDATE CASCADE ON DELETE CASCADE
    );

INSERT INTO roles(name)VALUES('owner');
INSERT INTO roles(name)VALUES ('farmer');
INSERT INTO roles(name)VALUES('inspector');
INSERT INTO roles(name)VALUES('transporter');
INSERT INTO roles(name)VALUES('merchant');

INSERT INTO userroles(userid,roleid)VALUES(1,1);
INSERT INTO userroles(userid,roleid)VALUES(2,2);
INSERT INTO userroles(userid,roleid)VALUES(3,3);
INSERT INTO userroles(userid,roleid)VALUES(3,5);
INSERT INTO userroles(userid,roleid)VALUES(4,4);
INSERT INTO userroles(userid,roleid)VALUES(5,5);
INSERT INTO userroles(userid,roleid)VALUES(6,5);
