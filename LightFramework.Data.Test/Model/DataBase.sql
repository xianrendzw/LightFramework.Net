/*==============================================================*/
/* Database name:  LightFramework                               */
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2011/8/18 16:25:59                           */
/*==============================================================*/


drop database if exists LightFramework;

/*==============================================================*/
/* Database: LightFramework                                     */
/*==============================================================*/
create database LightFramework;

use LightFramework;

/*==============================================================*/
/* Table: Account                                               */
/*==============================================================*/
create table Account
(
   Id                   int(4) not null auto_increment,
   FirstName            varchar(50) not null,
   LastName             varchar(50) not null,
   Email                varchar(50) not null,
   primary key (Id)
);

/*==============================================================*/
/* Table: Category                                              */
/*==============================================================*/
create table Category
(
   Id                   int(4) not null,
   Name                 varchar(50) not null,
   "Desc"               varchar(100) not null,
   primary key (Id)
);

/*==============================================================*/
/* Table: Item                                                  */
/*==============================================================*/
create table Item
(
   Id                   int(4) not null,
   ProductId            int(4) not null,
   listPrice            decimal not null,
   UnitCost             decimal not null,
   Currency             varchar(50) not null,
   Photo                varchar(100) not null,
   Quantity             int(4) not null,
   Attribute1           varchar(50) not null,
   Status               varchar(10) not null,
   primary key (Id)
);

/*==============================================================*/
/* Table: Nullable                                              */
/*==============================================================*/
create table Nullable
(
   Id                   int(4) not null auto_increment,
   TestBool             bool,
   TestByte             tinyint,
   TestChar             char,
   TestDateTime         datetime,
   TestDecimal          decimal,
   TestDouble           double,
   TestGuid             bigint,
   TestInt16            int(2),
   TestInt32            int(4),
   TestInt64            bigint(8),
   TestSingle           real,
   TestTimeSpan         timestamp,
   primary key (Id)
);

/*==============================================================*/
/* Table: Product                                               */
/*==============================================================*/
create table Product
(
   Id                   int(4) not null,
   CategoryId           int(4) not null,
   Name                 varchar(50) not null,
   "Desc"               varchar(50) not null,
   primary key (Id)
);

alter table Item add constraint FK_Reference_2 foreign key (ProductId)
      references Product (Id) on delete cascade on update restrict;

alter table Product add constraint FK_Reference_1 foreign key (CategoryId)
      references Category (Id) on delete restrict on update restrict;

