using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using OrchardCore.Users.Models;
using YesSql.Filters.Query;
using YesSql.Filters.Query.Services;
using YesSql;

namespace Fis.Users.Mapping
{
    public static class Extensions
    {
        public static User ToOrchardObject(this FisUser user)
        {
            return new User()
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                IsEnabled = user.IsEnabled,
                IsLockoutEnabled = user.IsLockoutEnabled,
                LockoutEndUtc = user.LockoutEndUtc,
                LoginInfos = user.LoginInfos,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Properties = user.Properties,
                ResetToken = user.ResetToken,
                RoleNames = user.RoleNames,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = true,
                UserClaims = user.UserClaims,
                UserId = user.UserId,
                UserName = user.UserName,
                UserTokens = user.UserTokens

            };
        }

        public static FisUser ToFisObject(this User user)
        {
            return new FisUser()
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                IsEnabled = user.IsEnabled,
                IsLockoutEnabled = user.IsLockoutEnabled,
                LockoutEndUtc = user.LockoutEndUtc,
                LoginInfos = user.LoginInfos,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Properties = user.Properties,
                ResetToken = user.ResetToken,
                RoleNames = user.RoleNames,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = true,
                UserClaims = user.UserClaims,
                UserId = user.UserId,
                UserName = user.UserName,
                UserTokens = user.UserTokens

            };
        }

        //public static OrchardCore.Users.ViewModels.UserIndexOptions ToOrchardObject(this Fis.Users.ViewModels.UserIndexOptions options)
        //{
        //    return new OrchardCore.Users.ViewModels.UserIndexOptions
        //    {
        //        UserSorts = options.UserSorts,
        //        BulkAction = options.BulkAction.ToOrchardObject(),
        //        SearchText = options.SearchText,
        //        RouteValues = options.RouteValues,
        //        OriginalSearchText = options.OriginalSearchText,
        //        Order = options.Order.ToOrchardObject(),
        //        FilterResult = options.FilterResult,
        //        Filter = options.Filter.ToOrchardObject(),
        //        EndIndex = options.EndIndex,
        //        SelectedRole = options.SelectedRole,
        //        StartIndex = options.StartIndex,
        //        TotalItemCount = options.TotalItemCount,
        //        UserFilters = options.UserFilters,
        //        UserRoleFilters = options.UserRoleFilters,
        //        UsersCount = options.UsersCount,
        //        UsersBulkAction = options.UsersBulkAction

        //    };
        //}

        //public static QueryFilterResult<User> ToOrchardObject(this QueryFilterResult<FisUser> result)
        //{
        //    var options = result.TermOptions;
        //    var convertedResult = new Dictionary<string, QueryTermOption<User>>();

        //    foreach (var option in options)
        //    {
        //        convertedResult.Add(option.Key, option.Value.ToOrchardObject());
        //    }

        //    return new QueryFilterResult<User>(convertedResult);
        //}

        //public static QueryTermOption<User> ToOrchardObject(this QueryTermOption<FisUser> option)
        //{
        //    IQuery<User> x = null;

        //    QueryTermOption<User> result = option;

        //    return result;
        //}

        public static OrchardCore.Users.ViewModels.UsersBulkAction ToOrchardObject(this Fis.Users.ViewModels.UsersBulkAction user)
        {
            return (OrchardCore.Users.ViewModels.UsersBulkAction)user;
        }

        public static OrchardCore.Users.ViewModels.UsersOrder ToOrchardObject(this Fis.Users.ViewModels.UsersOrder order)
        {
            return (OrchardCore.Users.ViewModels.UsersOrder)order;
        }

        public static OrchardCore.Users.ViewModels.UsersFilter ToOrchardObject(this Fis.Users.ViewModels.UsersFilter filter)
        {
            return (OrchardCore.Users.ViewModels.UsersFilter)filter;
        }

        //public static OrchardCore.Users.ViewModels.UserEntry ToOrchardObject(this Fis.Users.ViewModels.UserEntry entry)
        //{
        //    return new OrchardCore.Users.ViewModels.UserEntry
        //    {
        //        Shape = entry.Shape,
        //        UserId = entry.UserId
        //    };
        //}

        //public static Fis.Users.ViewModels.UserEntry ToFisObject(this OrchardCore.Users.ViewModels.UserEntry entry)
        //{
        //    return new Fis.Users.ViewModels.UserEntry
        //    {
        //        Shape = entry.Shape,
        //        UserId = entry.UserId
        //    };
        //}

        //public static List<OrchardCore.Users.ViewModels.UserEntry> ToOrchardObject(this List<Fis.Users.ViewModels.UserEntry> entries)
        //{
        //    var list = new List<OrchardCore.Users.ViewModels.UserEntry>();

        //    foreach (var entry in entries)
        //    {
        //        list.Add(entry.ToOrchardObject());
        //    }

        //    return list;
        //}

        //public static List<Fis.Users.ViewModels.UserEntry> ToFisObject(this List<OrchardCore.Users.ViewModels.UserEntry> entries)
        //{
        //    var list = new List<Fis.Users.ViewModels.UserEntry>();

        //    foreach (var entry in entries)
        //    {
        //        list.Add(entry.ToFisObject());
        //    }

        //    return list;
        //}
    }
}
