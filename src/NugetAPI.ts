import axios, {AxiosResponse} from "axios";
import {PackageVersionResponse} from "./interfaces/PackageVersionResponse";

/**
 * Makes various calls to the nuget.org API to collect information about nuget packages.
 */
export class NugetAPI {
	/**
	 * Returns a value indicating if a nuget package with the given name and version exists.
	 * @param packageName The name of the nuget package.
	 * @param version The version of the nuget package to check for.
	 * @returns An asynchronous operation with return type boolean that indicates if the nuget package with the version exists.
	 */
	public async versionExists (packageName: string, version: string): Promise<boolean> {
		// let response: AxiosResponse<PackageVersionResponse>;
		
		return await new Promise<boolean>((resolve, reject) => {
			let url: string = `https://api.nuget.org/v3-flatcontainer/${packageName}/index.json`;

			axios.get<PackageVersionResponse>(url)
				.then((response: AxiosResponse<PackageVersionResponse>)  => {
					let versions: string[] = response.data.versions;
					
					resolve(versions.includes(version));
				}, error => {
					reject(error);
				});
		});
	}
}