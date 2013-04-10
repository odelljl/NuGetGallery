﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet;

namespace NuGetGallery
{
    public interface IPackageService
    {
        PackageRegistration FindPackageRegistrationById(string id);
        Package FindPackageByIdAndVersion(string id, string version, bool allowPrerelease = true);
        IQueryable<Package> GetPackagesForListing(bool includePrerelease);
        IEnumerable<Package> FindPackagesByOwner(User user);
        IEnumerable<Package> FindDependentPackages(Package package);

        int CountFavorites(PackageRegistration packageRegistration);

        /// <summary>
        /// Populate the related database tables to create the specified packageRegistration for the specified user.
        /// </summary>
        /// <remarks>
        /// This method doesn't upload the packageRegistration binary to the blob storage. The caller must do it after this call.
        /// </remarks>
        /// <param name="nugetPackage">The packageRegistration to be created.</param>
        /// <param name="currentUser">The owner of the packageRegistration</param>
        /// <param name="commitChanges">Specifies whether to commit the changes to database.</param>
        /// <returns>The created packageRegistration entity.</returns>
        Package CreatePackage(INupkg nugetPackage, User user, bool commitChanges = true);

        /// <summary>
        /// Delete all related data from database for the specified packageRegistration id and version.
        /// </summary>
        /// <remarks>
        /// This method doesn't delete the packageRegistration binary from the blob storage. The caller must do it after this call.
        /// </remarks>
        /// <param name="id">Id of the packageRegistration to be deleted.</param>
        /// <param name="version">Version of the packageRegistration to be deleted.</param>
        /// <param name="commitChanges">Specifies whether to commit the changes to database.</param>
        void DeletePackage(string id, string version, bool commitChanges = true);

        void PublishPackage(string id, string version, bool commitChanges = true);
        void PublishPackage(Package package, bool commitChanges = true);

        void MarkPackageUnlisted(Package package, bool commitChanges = true);
        void MarkPackageListed(Package package, bool commitChanges = true);
        void AddDownloadStatistics(Package package, string userHostAddress, string userAgent, string operation);

        PackageOwnerRequest CreatePackageOwnerRequest(PackageRegistration package, User currentOwner, User newOwner);
        bool ConfirmPackageOwner(PackageRegistration package, User user, string token);
        void AddPackageOwner(PackageRegistration package, User user);
        void RemovePackageOwner(PackageRegistration package, User user);
    }
}