import { existsSync } from "https://deno.land/std@0.184.0/fs/exists.ts";

// Validate the arguments
if (Deno.args.length !== 2) {
    let errorMsg = "The 'validate-notes' cicd script must have two arguments.";
    errorMsg += "\nThe first arg must be either 'production', 'preview' or 'either'.";
    errorMsg += "\nThe second arg must be the version of the notes.";

    throw new Error(errorMsg);
}

const notesType: string = Deno.args[0].toLowerCase();
const version: string = Deno.args[1];

console.log("::group::Argument Values");
console.log(`Notes Type: ${notesType}`);
console.log(`Version: ${version}`);
console.log("::endgroup::");

if (notesType !== "production" && notesType !== "preview" && notesType != "either") {
    let errorMsg = "The notes type argument must be a value of 'production', 'preview' or 'either'.";
    errorMsg += "\nThe value is case-insensitive.";

    throw new Error(errorMsg);
}

const prodVersionRegex = /^v[0-9]+\.[0-9]+\.[0-9]+$/;
const prevVersionRegex = /^v[0-9]+\.[0-9]+\.[0-9]+-preview\.[0-9]+$/;

let isValid = false;

if (notesType === "production") {
    isValid = prodVersionRegex.test(version);
} else {
    isValid = prevVersionRegex.test(version);
}

if (isValid === false) {
    throw new Error(`The version is not in the correct ${notesType} version syntax.`);
}

let notesDirName = "";

if (notesType == "production" || notesType === "preview") {
    notesDirName = notesType === "production" ? "ProductionReleases" : "PreviewReleases";
} else {
    const isPrevTag: boolean = version.indexOf("-preview.") != -1;
    notesDirName = isPrevTag ? "PreviewReleases" : "ProductionReleases";
}

const notesFilePath = `${Deno.cwd()}/Documentation/ReleaseNotes/${notesDirName}/Release-Notes-${version}.md`;

if (!existsSync(notesFilePath)) {
    throw new Error(`The release notes '${notesFilePath}' do not exist.`);
}
