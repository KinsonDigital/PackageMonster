import {Action} from "./helpers/action";


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
			const inputValue: string = action.getInput("sample-input-name");

			if (inputValue === "hello") {
				action.setOutput("sample-output-name", "world");
			}

			return await Promise.resolve();
		} catch (error) {
			throw error;
		}
	}
}

const app: Application = new Application();
const action: Action = new Action;

app.main().then(() => {
	action.info("Action Success!!");
}, (error: Error) => {
	// Takes any incoming errors and fails the action with a message
	action.setFailed(error.message);
});
