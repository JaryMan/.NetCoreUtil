using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Util.Security.Identity.Models;

namespace Util.Security.Identity.Services.Implements {
    /// <summary>
    /// Identity用户服务
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TKey">用户标识类型</typeparam>
    public class IdentityUserManager<TUser, TKey> : AspNetUserManager<TUser> where TUser : User<TUser,TKey> {
        /// <exclude />
        public IdentityUserManager( IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger )
            : base( store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger ) {
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="tokenProvidor">令牌提供程序</param>
        /// <param name="token">令牌</param>
        /// <param name="newPassword">新密码</param>
        public async Task<IdentityResult> ResetPasswordAsync( TUser user, string tokenProvidor, string token, string newPassword ) {
            ThrowIfDisposed();
            if( user == null )
                throw new ArgumentNullException( nameof( user ) );
            if( !await VerifyUserTokenAsync( user, tokenProvidor, ResetPasswordTokenPurpose, token ) )
                return IdentityResult.Failed( ErrorDescriber.InvalidToken() );
            var result = await UpdatePasswordHash( user, newPassword, true );
            if( !result.Succeeded )
                return result;
            return await UpdateUserAsync( user );
        }
    }
}
