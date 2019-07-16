Select UserName, Name
                From AspNetUsers, AspNetUserRoles, AspNetRoles 
                Where AspNetUsers.Id = AspNetUserRoles.UserId AND AspNetRoles.Id = AspNetUserRoles.RoleId