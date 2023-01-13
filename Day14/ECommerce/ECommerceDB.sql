create database ecommerce;
use ecommerce;
CREATE TABLE roles (roleid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,  title VARCHAR(25));
CREATE TABLE categories(categoryid INT NOT NULL AUTO_INCREMENT PRIMARY KEY, title VARCHAR(50) NOT NULL);
CREATE TABLE accounts(accountid  INT NOT NULL PRIMARY KEY AUTO_INCREMENT, balance FLOAT);
CREATE TABLE carts(cartid INT NOT NULL PRIMARY KEY AUTO_INCREMENT, cartdate DATETIME NOT NULL, subtotal FLOAT);
CREATE TABLE users(userid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,password VARCHAR(100) NOT NULL,question VARCHAR(255),answer VARCHAR(255), roleid  int,
		   CONSTRAINT fk_userroles FOREIGN KEY(roleid) REFERENCES roles(roleid));
           
CREATE TABLE shippers(shipperid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,userid INT NOT NULL,
                       email VARCHAR(200) NOT NULL, contactnumber VARCHAR(10) NOT NULL, organization VARCHAR(50),
						CONSTRAINT fk_usersshippers FOREIGN KEY(userid) REFERENCES users(userid));

CREATE TABLE suppliers(supplierid INT NOT NULL AUTO_INCREMENT PRIMARY KEY,organization VARCHAR(100) NOT NULL,location VARCHAR(100) NOT NULL,
		        email VARCHAR(50) NOT NULL,contactnumber VARCHAR(10) NOT NULL, discounttype VARCHAR(50) NOT NULL,
				accountid INT NOT NULL,
                userid INT NOT NULL,
                CONSTRAINT fk_supplierusers FOREIGN KEY(userid) REFERENCES users(userid),
			    CONSTRAINT fk_supplieraccounts FOREIGN KEY(accountid) REFERENCES accounts(accountid));
CREATE TABLE products(productid INT NOT NULL AUTO_INCREMENT PRIMARY KEY, title VARCHAR(50) NOT NULL, picture VARCHAR(300) NOT NULL, description VARCHAR(300) NOT NULL,
 			unitprice INT NOT NULL,available BOOL NOT NULL,categoryid INT NOT NULL,
			CONSTRAINT fk_productscategories FOREIGN KEY(categoryid) REFERENCES categories(categoryid),unitinstock INT NOT NULL, 
			CONSTRAINT fk_productssuppliers FOREIGN KEY(categoryid) REFERENCES categories(categoryid));
CREATE TABLE customers(customerid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,firstname VARCHAR(100) NOT NULL,lastname VARCHAR(100),
			email VARCHAR(100) NOT NULL,contactnumber VARCHAR(10),address VARCHAR(255),
            roleid INT NOT NULL, accountid INT NOT NULL,userid INT NOT NULL,
            CONSTRAINT fk_customersusers FOREIGN KEY(userid) REFERENCES users(userid),
			CONSTRAINT fk_customeraccounts FOREIGN KEY(accountid) REFERENCES accounts(accountid),
            CONSTRAINT fk_customersroles FOREIGN KEY(roleid) REFERENCES roles(roleid));
CREATE TABLE orders (orderid INT NOT NULL PRIMARY KEY AUTO_INCREMENT, orderdate DATETIME NOT NULL, status VARCHAR(20), customerid INT NOT NULL,
			CONSTRAINT fk_orderscustomers FOREIGN KEY(customerid) REFERENCES customers(customerid));
CREATE TABLE cartdetails(cartdetailid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
						cartid INT NOT NULL, productid INT NOT NULL, quantity INT NOT NULL, 
						CONSTRAINT fk_cartscartdetails FOREIGN KEY(cartid) REFERENCES carts(cartid),
                        CONSTRAINT fk_productscartdetails FOREIGN KEY(productid) REFERENCES products(productid));
CREATE TABLE orderdetails (orderdetailid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
			  orderid INT NOT NULL, productid INT NOT NULL, quantity INT,
			  CONSTRAINT fk_orderdetailsorders FOREIGN KEY (orderid) REFERENCES orders(orderid),
              CONSTRAINT fk_orderdetailsproducts FOREIGN KEY (productid) REFERENCES products(productid));
CREATE TABLE deliveries(deliveryid INT NOT NULL PRIMARY KEY AUTO_INCREMENT, status VARCHAR(200) NOT NULL, orderid INT NOT NULL,shipperid INT NOT NULL,
			CONSTRAINT fk_deliveryorders FOREIGN KEY(orderid) REFERENCES orders(orderid),
			CONSTRAINT fk_deliveryshippers FOREIGN KEY(shipperid) REFERENCES shippers(shipperid));
CREATE TABLE ledger(transactionid INT NOT NULL PRIMARY KEY AUTO_INCREMENT,transactiondate DATETIME,accountid INT, amount INT, paymentmode VARCHAR(50),
			CONSTRAINT fk_ledgeraccounts FOREIGN KEY(accountid) REFERENCES accounts(accountid));
            
CREATE TABLE payments(paymentid INT NOT NULL PRIMARY KEY AUTO_INCREMENT, paymentdate DATETIME,orderid INT, amount float, paymentmode varchar(50),transactionid  INT NOT NULL,
			CONSTRAINT fk_paymentstransactions FOREIGN KEY(transactionid) REFERENCES ledger(transactionid),
			CONSTRAINT fk_paymentsorders FOREIGN KEY(orderid) REFERENCES orders(orderid));
           
CREATE TABLE tasks(id int NOT NULL PRIMARY KEY AUTO_INCREMENT,task varchar(200) NOT NULL,
				  status tinyint NOT NULL,created_at datetime NOT NULL);
           

INSERT INTO tasks (id, task, status, created_at) VALUES(1, 'Find bugs', 1, '2019-04-10 23:50:40');
INSERT INTO tasks (id, task, status, created_at) VALUES(2, 'Review code', 1, '2019-04-10 23:50:40');
INSERT INTO tasks (id, task, status, created_at) VALUES(4, 'Refactor Code', 1, '2019-04-10 23:50:40');
INSERT INTO tasks (id, task, status, created_at) VALUES(5, 'complete handson', 1, '2019-04-10 23:50:50');
INSERT INTO tasks (id, task, status, created_at) VALUES(6, 'create repository', 1, '2019-04-01 14:22:25');
INSERT INTO tasks (id, task, status, created_at) VALUES(11, 'Help students', 1, '2019-11-13 06:06:12');
INSERT INTO tasks (id, task, status, created_at) VALUES(14, 'Conduct Interviews', 1, '2019-11-13 19:23:23');

INSERT INTO categories(title) VALUES ("Electronics");
INSERT INTO categories(title) VALUES ("Clothing");
INSERT INTO categories(title) VALUES ("Jewelry");
INSERT INTO categories(title) VALUES ("Toys");
INSERT INTO categories(title) VALUES ("Computers");
INSERT INTO categories(title) VALUES ("Personalcare");
INSERT INTO categories(title) VALUES ("Flowers");
INSERT INTO categories(title) VALUES ("Fruits");
INSERT INTO categories(title) VALUES ("Vegetables");

INSERT INTO roles(title)VALUES("customer");
INSERT INTO roles(title)VALUES("bod");
INSERT INTO roles(title)VALUES("staff");
INSERT INTO roles(title)VALUES("supplier");
INSERT INTO roles(title)VALUES("vendor");
INSERT INTO roles(title)VALUES("shipper");



INSERT INTO products(title,picture,description,unitprice,available,categoryid,unitinstock)
values ("Television","/assets/images/gerbera.jpg", "best televiosn in market with good disply and speaker quality",18000, 1,1,200);
INSERT INTO products(title,picture,description,unitprice,available,categoryid,unitinstock)
values ("nike dri fit t shirt","/assets/images/lotus.jpg", "Dri-fit fabric keeps away sweat to help keep you comfortable and dry",350, 1,2,1500);
INSERT INTO products(title,picture,description,unitprice,available,categoryid,unitinstock)
values ("Pendant","/assets/images/lily.jpg", "A pendant is a loose-hanging piece of jewellery,with pure quality",70000, 1,3,50);INSERT INTO products (title,picture,description,unitprice,available,categoryid,unitinstock)
values ("teddy bear","/assets/images/rose.jpg", "best and smooth teddy bear for your child",200, 0,4,5220);
INSERT INTO products (title,picture,description,unitprice,available,categoryid,unitinstock)
values ("HP NoteBook ","/assets/images/jasmine.jpg", "HP NoteBook is a Windows 10 laptop with a 15.60-inch display that has a resolution of 1366x768 pixels. It is powered by a Core i5 processor and it comes with 8GB of RAM",40000, 1,5,4555);
 