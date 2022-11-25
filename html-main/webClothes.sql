
create table products(
	id_products int identity(1,1) primary key,
	id_warehouse int,
	id_category int,	
	id_brand int,
	name nvarchar(255),
	price nvarchar(255),
	discount int,
	images nvarchar (255),
)
create table categories(
	id_category int identity(1,1) primary key,
	name_category nvarchar(255),
)
create table warehouse(
	id_warehouse int identity(1,1) primary key,
	id_products int,
	quantity int,
)
create table brand(
	id_brand int identity(1,1) primary key,
	name_brand nvarchar(255),
)
create table partners(
	id_partners int identity (1,1) primary key,
	name_partners nvarchar(255),
	phone_partners int,
	email_partners varchar(255),
	require_partners nvarchar(255), 
)
create table constracts(
	id_constracts int identity(1,1) primary key,
	id_partners int,
	id_products int,
	price_constracts varchar(255),
	quantity int,
)
create table customers(
	id_customer int identity (1,1) primary key,
	name_customer nvarchar(255),
	phone_customer int,
	email_customer varchar(255),
	password_customer varchar(255),
	comfirm_password_customer varchar(255),
	gender_customer nvarchar(255),
	age_customer int,
	address_customer nvarchar(255),
)

create table customer_order(
	id_order int identity (1,1) primary key,
	id_customer int,
	id_products	int,		
	date_buy datetime,
	price float(20),
	states bit default 0,
	date_deli datetime,
	name_customer nvarchar(255),
	address_customer nvarchar(255),
	phone_customer int,	
	status_paying varchar(255),
	status_deli varchar(255),
)
create table details_order(
	id_order int identity (1,1) primary key,
	id_products int,
	quantity_order int,
	unit_price float(20),
	total float(20),
)	
create table reports(
	id_reports int identity (1,1) primary key,
	id_customer int,
	subject_customer nvarchar(255),
	contents_customer nvarchar(255),
)
alter table products
add foreign key (id_category) references categories(id_category) 

alter table products
add foreign key (id_warehouse) references warehouse(id_warehouse)

alter table products
add foreign key (id_brand) references brand(id_brand)

alter table constracts
add foreign key(id_partners) references partners(id_partners)

alter table constracts 
add foreign key(id_products) references products(id_products)

alter table customer_order
add foreign key(id_customer) references customers(id_customer)

alter table customer_order
add foreign key(id_products) references products(id_products)

alter table reports
add foreign key (id_customer) references customers(id_customer)

alter table details_order
add foreign key(id_products) references products(id_products)