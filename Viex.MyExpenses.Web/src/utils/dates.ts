export default {
    short(date: string | Date) {
        const finalDate = new Date(date);

        const mm = finalDate.getMonth() + 1;
        const dd = finalDate.getDate();

        return [
            finalDate.getFullYear(),
            (mm > 9 ? "" : "0") + mm,
            (dd > 9 ? "" : "0") + dd
        ].join('-');
    }
};
