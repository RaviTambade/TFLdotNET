create database transflower
use  transflower;
create table departments(id int not null primary key auto_increment,
                         name varchar(255),     
                         location varchar(255));

create table employees(id int not null primary key auto_increment,
                       firstName varchar(255),
                       lastName varchar(255),
                       email varchar(255),
                       address varchar(255),
                       password varchar(25) not null,
                       deptid int not null,
                       managerid int,
                       constraint fk_dept foreign key (deptid) references departments(id) on update cascade on delete cascade);

create table if not exists roles (
                        roleid int unsigned not null auto_increment,
                        rolename varchar(100) not null,
                        primary key(roleid),
                        unique(rolename)
                        );

create table if not exists emp_roles (
                        id int not null primary key auto_increment,
                        empid int  not null,
                        roleid int UNSIGNED not null,
                        constraint fk_emp foreign key (empid) references employees(id) on update CASCADE,
                        constraint fk_roles foreign key (roleid)references roles(roleid) on update CASCADE
                        );

                
INSERT INTO roles(rolename) VALUES ('Head'),('Manager');
INSERT INTO roles(rolename) VALUES ('Accountant'),('Trainer');
INSERT INTO roles(rolename) VALUES ('Intern'),('Tester');

INSERT INTO departments(name,location) VALUES('IT','Pune'), ('Marketing','Nashik'), ('Training','Mumbai'),('PMO','Mumbai');

INSERT INTO employees(firstName,lastName,email,address,password,deptid, managerid)  
		VALUES('Rajiv','Pande','rajiv.pande@gmail.com','Manchar','Rajiv@123',1,1),
			  ('Shiv','Jadhav','Siv.kumar@gmail.com','Bhavadi','Shiv23@34',1,1),
              ('Manish','Varma','rajiv.pande@gmail.com','Manchar','Rajiv@123',1,2),
			  ('Rutuja','Patil','Siv.kumar@gmail.com','Bhavadi','Shiv23@34',1,2),
              ('Kalyani','Kulkarni','rajiv.pande@gmail.com','Manchar','Rajiv@123',1,3),
			  ('Sham','Jadhav','Siv.kumar@gmail.com','Bhavadi','Shiv23@34',1,2),
              ('Chaitanya','Gore','rajiv.pande@gmail.com','Manchar','Rajiv@123',1,3),
              ('Riyaz','Shaikh','Siv.kumar@gmail.com','Bhavadi','Shiv23@34',1,3),
              ('Ravindar','Singh','rajiv.pande@gmail.com','Manchar','Rajiv@123',1,2),
              ('Sham','Murthy','Siv.kumar@gmail.com','Bhavadi','Shiv23@34',1,1),
              ('Chaitanya','Pandit','rajiv.pande@gmail.com','Manchar','Rajiv@123',1,2),
			  ('Randhir','Jadhav','Siv.kumar@gmail.com','Bhavadi','Shiv23@34',1,1),
              ('Gautam','Raman','ganesh.shinde@gmail.com','Karegoan','GaneshShinde56',1,2);


INSERT INTO emp_roles(empid,roleid) VALUES (1,1),(1,2),(2,2),(3,1),(4,2),(5,2);
