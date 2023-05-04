// Validate the arguments
if (Deno.args.length !== 3) {
    let errorMsg = "The 'validate-tag' cicd script must have two arguments.";
    errorMsg += "\nThe first arg must be either 'production', 'preview' or 'either'.";
    errorMsg += "\nThe second arg must be the name of the tag.";

    throw new Error(errorMsg);
}

const tagType: string = Deno.args[0].toLowerCase();
const tag: string = Deno.args[1].startsWith("v") ? Deno.args[1] : `v${Deno.args[1]}`;
const projectName: string = Deno.args[2];

console.log("::group::Argument Values")
console.log(`Tag Type: ${tagType}`);
console.log(`Tag: ${tag}`);
console.log(`Project Name: ${projectName}`);
console.log("::endgroup::");

if (tagType !== "production" && tagType !== "preview" && tagType !== "either") {
    let errorMsg = "The tag type argument must be a value of 'production', 'preview' or 'either'.";
    errorMsg += "\nThe value is case-insensitive.";

    throw new Error(errorMsg);
}

const prodVersionRegex = /^v[0-9]+\.[0-9]+\.[0-9]+$/;
const prevVersionRegex = /^v[0-9]+\.[0-9]+\.[0-9]+-preview\.[0-9]+$/;

let isValid = false;

switch (tagType) {
    case "production":
        isValid = prodVersionRegex.test(tag);
        break;
        case "preview":
        isValid = prevVersionRegex.test(tag);
        break;
    case "either":
        isValid = prodVersionRegex.test(tag) || prevVersionRegex.test(tag);
        break;
    default:
        break;
}
        
if (isValid === false) {
    const tagTypeStr = tagType === "production" || tagType === "preview"
    ? tagType
    : "production or preview";
    
    throw new Error(`The tag is not in the correct ${tagTypeStr} version syntax.`);
}
        
const tagsUrl = `https://api.github.com/repos/KinsonDigital/${projectName}/tags`;
const response = await fetch(tagsUrl);
const responseData = <{ name: ""}[]>await response.json();

const tags: string[] = responseData.map((tagObj) => tagObj.name);

const tagExists: boolean = tags.some(t => t === tag);

if (tagExists) {
    throw new Error(`The tag '${tag}' already exists.`);
}
