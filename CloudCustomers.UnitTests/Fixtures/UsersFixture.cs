using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloudcustomers.API.Models;


namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture{
        public static List<User>GetTestUsers() => new() { 
            new User{
                Name="Toralf",
                Email="toralf@example.com",
                Address =new Address{
                    Street="storheilia 28",
                    City="Bergen",
                    ZipCode="5050"
                }
            },
            new User{
                Name="Halvor",
                Email="halvor@example.com",
                Address =new Address{
                    Street="bjornelia 60",
                    City="Oslo",
                    ZipCode="1055"
                }
            },
             new User{
                Name="Jostein",
                Email="jostein@example.com",
                Address =new Address {
                    Street="bjørnetreet 28",
                    City="Bergen",
                    ZipCode="5035"
                }
            }
        };

    }
}
