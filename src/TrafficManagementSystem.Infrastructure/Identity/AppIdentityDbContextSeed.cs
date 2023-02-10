using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrafficManagementSystem.Domain.Entities;
using TrafficManagementSystem.Infrastructure.DbContexts;
using TrafficManagementSystem.Infrastructure.Models;

namespace TrafficManagementSystem.Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Adeola",
                    LastName = "Odeku",
                    Email = "andrew@gmail.com",
                    UserName = "andrew",
                    Address ="No 10 Odeku,Lagos",
                    EmailConfirmed=true,
                    
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }

        public static async Task SeedOffenceTypes(TrafficManagementSystemDbContext dbContext)
        {
            var jsonData = "[ { \"Name\": \"Marshal Assault on Duty\", \"Code\": \"MAD\", \"Point\": 10, \"FineAmount\": 15000, \"Category\": 2 }, { \"Name\": \"Corrupt Attempt of Marshal on Duty\", \"Code\": \"CAM\", \"Point\": 10, \"FineAmount\": 15000, \"Category\": 2 }, { \"Name\": \"Sign Violation Causes\", \"Code\": \"SVC\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 3 }, { \"Name\": \"Area Speed Construction Limit Violation\", \"Code\": \"ASC\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Dangerous Driving\", \"Code\": \"DDG\", \"Point\": 10, \"FineAmount\": 55000, \"Category\": 1 }, { \"Name\": \"Don’t Move Violation\", \"Code\": \"DMV\", \"Point\": 2, \"FineAmount\": 3000, \"Category\": 2 }, { \"Name\": \"License Violation Driver\", \"Code\": \"LVD\", \"Point\": 10, \"FineAmount\": 11000, \"Category\": 2 }, { \"Name\": \"Driving under Drug/Alcohol Influence\", \"Code\": \"DAI\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 2 }, { \"Name\": \"Driving with Worn-out Tyre\", \"Code\": \"DWT\", \"Point\": 3, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Driving without/with Spare/Expired Tyre\", \"Code\": \"DWS\", \"Point\": 2, \"FineAmount\": 6000, \"Category\": 3 }, { \"Name\": \"Extreme Smoke Emission\", \"Code\": \"ESE\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Failure to Cover Defective material\", \"Code\": \"FCD\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Failure to Fix-Red-Flag on Anticipated Road\", \"Code\": \"FFA\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Failure to Move Forward\", \"Code\": \"FMF\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Failure to Report Crash\", \"Code\": \"FRC\", \"Point\": 10, \"FineAmount\": 25000, \"Category\": 1 }, { \"Name\": \"Violation on Fire Extinguisher\", \"Code\": \"VFE\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 3 }, { \"Name\": \"Insufficient Construction Warning-Sign\", \"Code\": \"ICW\", \"Point\": 3, \"FineAmount\": 55000, \"Category\": 1 }, { \"Name\": \"Sign/Light Violation\", \"Code\": \"SLV\", \"Point\": 2, \"FineAmount\": 3000, \"Category\": 2 }, { \"Name\": \"Medical Hospital Rejection of Personnel/ Road Crash Victim\", \"Code\": \"MHR\", \"Point\": 3, \"FineAmount\": 55000, \"Category\": 1 }, { \"Name\": \"Operating Deficient Mechanically Vehicle\", \"Code\": \"ODM\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Marshal Obstructing on Duty\", \"Code\": \"MOD\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 2 }, { \"Name\": \"Operating Forged Document Vehicle\", \"Code\": \"OFV\", \"Point\": 10, \"FineAmount\": 25000, \"Category\": 2 }, { \"Name\": \"Overloading passengers\", \"Code\": \"OVP\", \"Point\": 10, \"FineAmount\": 15000, \"Category\": 1 }, { \"Name\": \"Violation of Passenger Manifest\", \"Code\": \"VPM\", \"Point\": 10, \"FineAmount\": 15000, \"Category\": 1 }, { \"Name\": \"Riding Motorcycle without Crash-Helmet\", \"Code\": \"RMC\", \"Point\": 2, \"FineAmount\": 3000, \"Category\": 1 }, { \"Name\": \"Road/Highway Obstruction\", \"Code\": \"RHO\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Violation of Road Marking\", \"Code\": \"VRM\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Route Violation\", \"Code\": \"RVL\", \"Point\": 10, \"FineAmount\": 15000, \"Category\": 1 }, { \"Name\": \"Violation of Seat Belt\", \"Code\": \"VSB\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Violation Speed Limit\", \"Code\": \"VSL\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Unauthorized Tampering/removal of Road Signs\", \"Code\": \"UTR\", \"Point\": 5, \"FineAmount\": 6000, \"Category\": 1 }, { \"Name\": \"Under Age Riding/Driving\", \"Code\": \"UAD\", \"Point\": 3, \"FineAmount\": 15000, \"Category\": 1 }, { \"Name\": \"Using Phone while Driving Violation\", \"Code\": \"UPD\", \"Point\": 4, \"FineAmount\": 5000, \"Category\": 1 }, { \"Name\": \"Violation of Driving License\", \"Code\": \"VDL\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 2 }, { \"Name\": \"Violation of Plate Number\", \"Code\": \"VPN\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Windshield Vehicle Violation\", \"Code\": \"WVV\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Overtaking Wrongful\", \"Code\": \"OVW\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Carrying Load Above Limit\", \"Code\": \"CAL\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Violation of Vehicle Mirror\", \"Code\": \"VVM\", \"Point\": 3, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Violation of Learner Driving Regulation\", \"Code\": \"VLD\", \"Point\": 10, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Violation of Child Restraint\", \"Code\": \"VCR\", \"Point\": 6, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Violation of Child Sitting–Position\", \"Code\": \"VCP\", \"Point\": 6, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Right-Hand Driving Violation Vehicle\", \"Code\": \"RDV\", \"Point\": 10, \"FineAmount\": 4000, \"Category\": 1 }, { \"Name\": \"Violation of Other Offence\", \"Code\": \"VOO\", \"Point\": 2, \"FineAmount\": 4000, \"Category\": 1 } ]";
            var offenceTypes = JsonSerializer.Deserialize<OffenceType[]>(jsonData);
            foreach (var offenceType in offenceTypes!)
            {
                if (await dbContext!.OffenceTypes!.AnyAsync(t => t.Name==offenceType.Name)) continue;
                await dbContext!.OffenceTypes!.AddAsync(offenceType);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
