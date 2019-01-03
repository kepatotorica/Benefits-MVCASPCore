	create table admin (
		a_id serial PRIMARY KEY,
		username VARCHAR(50),
		password VARCHAR(50),
		email varchar(50)
	);
	
	insert into admin (a_id, username, password, email) values (1, 'kepa', '2111734a2ac9c405a963a124beb8d1e2', 'kepatoto@gmail.com');

	ALTER SEQUENCE admin_a_id_seq RESTART WITH 2;