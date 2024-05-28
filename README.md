# joBoard

## Database Setup

### Prerequisites
- Install Microsoft SQL Server: [Download Link](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Install SQL Server Management Studio (SSMS): [Download Link](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### Step-by-Step Setup

1. **Install Microsoft SQL Server and SSMS**
   - Follow the installation instructions provided on the Microsoft website.
   - Ensure SQL Server is running and accessible.

2. **Create the Database**
   - Open SQL Server Management Studio (SSMS).
   - Connect to your SQL Server instance.
   - Right-click on the `Databases` node and select `New Database`.
   - Enter a name for your database (e.g., `MyDatabase`) and click `OK`.

3. **Run Database Scripts**
   - In SSMS, open a new query window.
   - Copy and paste the contents of `setup.sql` (provided in this repository) into the query window.
   - Execute the script by pressing `F5` or clicking the `Execute` button.

4. **Configure Database Connection**
   - Copy the sample configuration file provided in the repository:
     ```bash
     cp config.sample.yml config.yml
     ```
   - Open `config.yml` and update the database connection settings with your SQL Server credentials.

   ```yaml
   default: &default
     adapter: sqlserver
     encoding: utf8
     pool: 5
     host: <your_sql_server_host>
     username: <your_sql_server_username>
     password: <your_sql_server_password>
     database: MyDatabase

   development:
     <<: *default

   test:
     <<: *default

   production:
     <<: *default
