use master
go
if exists (select * from sysdatabases where name = 'Database-web-clothes')
	{
		drop database Database-web-clothes
	}
create table Khachhang{
	Ten Char(10),
	string
}