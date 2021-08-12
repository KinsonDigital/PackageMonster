import {Action} from "./helpers/Action";
import {NugetAPI} from "./NugetAPI";


/**
 * The main GitHub action.
 */
export class Application {
	/**
	 * The main entry point of the GitHub action.
	 * @returns {Promise<void>} Returns just a promise with now result.
	 */
	public async main (): Promise<void> {
		const action: Action = new Action();
		
		try {
			// Refer to the action.yml file for the list of inputs setup for the action
			const packageName: string = action.getInput("nuget-package-name");
			const version: string = action.getInput("version");
			const shouldFailInput: string = action.getInput("fail-if-version-exists");
			let shouldFail: boolean = false;

			// if the should fail input is null, undefined or empty, treat it as false
			if (shouldFailInput != null &&
				shouldFailInput != undefined &&
				shouldFailInput != "") {
				if (shouldFailInput.toLowerCase() === "true") {
					shouldFail = true;
				} else if (shouldFailInput.toLowerCase() != "false") {
					let errorMsg: string = `The 'fail-if-version-exists' value of '${shouldFailInput}' is invalid.`;
					errorMsg += "\n\tThe input only accepts non case sensitive string values of 'true' or 'false'.";

					action.setFailed(errorMsg);
				}
			}

			let nuget: NugetAPI = new NugetAPI();
			let exists: boolean = await nuget.versionExists(packageName.toLowerCase(), version);

			// Only fail the action if the user is setting it to fail when the package version exists
			// as well as if the package actually does exist.
			if (shouldFail && exists) {
				action.setFailed(`The nuget package '${packageName}' with version '${version}' already exists.`);
			} else {
				action.info(`The nuget package '${packageName}' with version '${version}' ${exists ? "exists." : "does not exist."}`);
			}
			
			action.setOutput("version-exists", exists === true ? "true" : "false");

			return await Promise.resolve();
		} catch (error) {
			throw error;
		}
	}
}

const app: Application = new Application();
const action: Action = new Action;

app.main().then(() => {
	action.info("\nAction Success!!");
}, (error: Error) => {
	// Takes any incoming errors and fails the action with a message
	action.setFailed(error.message);
});
