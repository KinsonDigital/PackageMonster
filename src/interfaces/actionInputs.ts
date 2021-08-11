/**
 * Represents the inputs of the action.
 */
export interface ActionInputs {
	/**
     * The current environment.
     * @summary The values 'dev' and 'develop' are valid values for the development environment.
     * The values 'prod', 'production', undefined, or empty all represents the production environment.
     * This value is not case sensitive.
	 * 
	 * NOTE: The input 'environment' is just for testing and never exists in the YAML file.
     */
	environment: string,
	
	// TODO: Add more action inputs here
}