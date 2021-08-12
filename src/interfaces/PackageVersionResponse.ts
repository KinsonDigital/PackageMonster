/**
 * The package version response from the Nuget API call.
 */
export interface PackageVersionResponse {
	/**
	 * The list of nuget package versions for a package in the nuget.org repository.
	 */
	versions: string[];
}