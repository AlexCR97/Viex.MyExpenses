const monthNames = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", ]
const dayNames = [ 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday' ];

export default {
    addDays(date: Date, days: number): Date {
        const newDate = new Date(date.valueOf());
        const currentDays = newDate.getDate();
        newDate.setDate(currentDays + days);
        return newDate;
    },

    /**
     * Gets the ISO week number of the current year
     *
     * Based on information at:
     *
     *    http://www.merlyn.demon.co.uk/weekcalc.htm#WNR
     *
     * Algorithm is to find nearest thursday, it's year
     * is the year of the week number. Then get weeks
     * between that date and the first day of that year.
     *
     * Note that dates in one year can be weeks of previous
     * or next year, overlap is up to 3 days.
     *
     * e.g.
     * 
     * 2014/12/29 is Monday in week  1 of 2015.
     * 
     * 2012/1/1   is Sunday in week 52 of 2011
     */
     currentWeek() {
        const today = new Date();
        const d = new Date(Date.UTC(today.getFullYear(), today.getMonth(), today.getDate()));
        const dayNum = d.getUTCDay() || 7;
        d.setUTCDate(d.getUTCDate() + 4 - dayNum);
        const yearStart = new Date(Date.UTC(d.getUTCFullYear(),0,1));
        return Math.ceil(((((d as any) - (yearStart as any)) / 86400000) + 1)/7)
    },

    dayName(date: Date | string): string {
        if (typeof(date) == 'string')
            date = new Date(date)
        
        const day = date.getDay()
        return dayNames[day]
    },

    firstOfCurrentWeek(): Date {
        const today = new Date();
        const first = today.getDate() - today.getDay();
        return new Date(today.setDate(first));
    },
    
    lastOfCurrentWeek(): Date {
        const today = new Date();
        const first = today.getDate() - today.getDay();
        const last = first + 6;
        return new Date(today.setDate(last));
    },

    monthName(date: Date | string): string {
        if (typeof(date) == 'string')
            date = new Date(date)
        
        const month = date.getMonth()
        return monthNames[month];
    },

    short(date: string | Date): string {
        const finalDate = new Date(date);

        const mm = finalDate.getMonth() + 1;
        const dd = finalDate.getDate();

        return [
            finalDate.getFullYear(),
            (mm > 9 ? "" : "0") + mm,
            (dd > 9 ? "" : "0") + dd
        ].join('-');
    },
};
