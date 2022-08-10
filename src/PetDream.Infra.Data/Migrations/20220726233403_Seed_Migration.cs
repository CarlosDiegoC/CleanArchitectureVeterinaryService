using Microsoft.EntityFrameworkCore.Migrations;

namespace PetDream.Infra.Data.Migrations
{
    public partial class Seed_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO `pets` (`Id`, `Name`, `Breed`, `Color`, `PetOwnerId`, `BirthDate`, `Gender`, `Status`) VALUES ('1', 'Duke', 'Dalmatian', 'White', '1', '2014-08-10 10:00:00.000000', 'Male', '1')");
            migrationBuilder.Sql("INSERT INTO `pets` (`Id`, `Name`, `Breed`, `Color`, `PetOwnerId`, `BirthDate`, `Gender`, `Status`) VALUES ('2', 'Duna', 'Dalmatian', 'White', '1', '2016-02-01 10:00:00.000000', 'Female', '1')");
            migrationBuilder.Sql("INSERT INTO `pets` (`Id`, `Name`, `Breed`, `Color`, `PetOwnerId`, `BirthDate`, `Gender`, `Status`) VALUES ('3', 'Rex', 'Rottweiler', 'Black', '2', '2018-03-21 10:00:00.000000', 'Male', '1')");
            migrationBuilder.Sql("INSERT INTO `veterinarycarerecords` (`Id`, `ServiceDate`, `Diagnosis`, `PetId`, `VeterinarianId`, `PetObservations`, `PetWeight`) VALUES ('1', '2022-07-19 13:00:00.000000', 'The dog is healthy', '1', '1', 'Alergic to corticoids', '30.15')");            
            migrationBuilder.Sql("INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnabled`, `AccessFailedCount`) VALUES ('afc3acfd-03cc-4b8e-ac0f-28cc172c23ce', 'fernandapessoa@gft.com', 'FERNANDAPESSOA@GFT.COM', 'fernandapessoa@gft.com', 'FERNANDAPESSOA@GFT.COM', '0', 'AQAAAAEAACcQAAAAELnsW9VmzvRMW/4JAp7um67gm2ThaRZZlF/PBrFn/VzXFMtv07dUm873ZNO034Noaw==', 'CGR4AQWMZA4KEVKRSQQTN5LO2NZCCHOO', 'b2a0bbc0-6072-4a1f-8385-be9ff5343272', '0', '0', '1', '0')");
            migrationBuilder.Sql("INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnabled`, `AccessFailedCount`) VALUES ('0f5c330e-c0cf-4567-97c9-4affe8605375', 'carlosdiego@gft.com', 'CARLOSDIEGO@GFT.COM', 'carlosdiego@gft.com', 'CARLOSDIEGO@GFT.COM', '0', 'AQAAAAEAACcQAAAAEDCGm52xr3Wv3PSaPiQ9N4aFGuwHmqaCPvDiNKF5OIVsnASvKo85s1I17+lwwYOKgQ==', 'C66X3BLVDTAO4IZ4UWFFKZSZIYMDQKUY', '5073354b-5849-4cec-894a-81e795fe0063', '0', '0', '1', '0')");
	        migrationBuilder.Sql("INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnabled`, `AccessFailedCount`) VALUES ('7a0e8c27-8404-491b-9dea-3ae8472876e8', 'marciabezerra@gmail.com', 'MARCIABEZERRA@GMAIL.COM', 'marciabezerra@gmail.com', 'MARCIABEZERRA@GMAIL.COM', '0', 'AQAAAAEAACcQAAAAEMx8qwu0kZaTSdyMoczSsv4zOKD4nuzqkO0thrPTq8Ae7y3a/iC2YdGO7Solqpwnpw==', 'ML4AWY7GII4R77MDC4F2O32S2JL34U2N', '3d505e98-5f67-4e3f-aff6-3c1afb3825d7', '0', '0', '1', '0')");
	        migrationBuilder.Sql("INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnabled`, `AccessFailedCount`) VALUES ('da667396-2841-4d7f-9fb8-142c5237b6b9', 'mayaracosta@gmail.com', 'MAYARACOSTA@GMAIL.COM', 'mayaracosta@gmail.com', 'MAYARACOSTA@GMAIL.COM', '0', 'AQAAAAEAACcQAAAAEOexf7dTjVE7OfJ5usGRRScL8fCNuFDOlYGo8/wANQkYOm2ief3rdPPt0yReINr5/w==', 'K3VECIXC6KE6ERYCK7LP2P6VKV5IE63G', '6c4fec8f-7e19-4663-a146-5962423f96bf', '0', '0', '1', '0')");

                        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM pets");
            migrationBuilder.Sql("DELETE FROM veterinarycarerecords");
            migrationBuilder.Sql("DELETE FROM aspnetusers");
        }
    }
}
