->At startup:
	->the database is updated to the last migration.
	->the roles (Admin, User) and un User (Admin) with default credentials (stored in appsettings.json) are seeded to the database.

->Please, add your personal AppId and AppSecret for Facebook in StartUp.cs to test External login functionality.