export enum TextCase {
    capitalized,
    dash,
};

export default {
    between(source: string, startDelimeter: string, endDelimeter: string, options?: { includeDelimeters?: boolean }) {
        const escape = (str: string) => `\\${str}`;
        const optionalLineBreak = escape('n?');
        const regex = new RegExp(`${escape(startDelimeter)}(.*?)${escape(endDelimeter)}${optionalLineBreak}`, "g");
        const matches = source.match(regex);

        if (matches == undefined || matches == null) {
            return [];
        }

        let matchStrings = matches.map(match => match.toString());
        
        // Remove delimeters if not specified
        if (!options || !options.includeDelimeters) {
            matchStrings = matchStrings.map(str => this.removeCharacters(str, [ startDelimeter, endDelimeter ]));
        }

        return matchStrings;
    },

    case(source: string, fromCase: TextCase, toCase: TextCase) {
        if (fromCase == TextCase.dash && toCase == TextCase.capitalized)
            return fromDashCaseToCapitalizedCase(source);

        throw Error("Unsupported text case conversion");
    },

    hasLineBreaks(text: string): boolean {
        const match = /\r|\n/.exec(text);

        if (match)
            return true;

        return false;
    },

    percentage(num: number) {
        if (num < 0 || num > 1)
            throw new Error("Cannot convert number to percentage. Must be between 0 and 1.");
        
        return (num * 100).toFixed(2) + "%"
    },

    removeCharacters(str: string, characters: string[]) {
        characters.forEach(c => str = str.replace(c, ""));
        return str;
    },
}

/**
 * Takes a string with a dash case formatting (this-is-dash-case)
 * and transforms it to a string with a capitalized case formatting (This Is A Capitalized Case)
 * @param str A string with dash case formatting
 * @returns A string with capitalized case formatting
 */
function fromDashCaseToCapitalizedCase(str: string): string {
    const splitted = str.split("-");
    const capitalized = splitted.map(s => s[0].toUpperCase() + s.slice(1));
    return capitalized.join(" ");
}
