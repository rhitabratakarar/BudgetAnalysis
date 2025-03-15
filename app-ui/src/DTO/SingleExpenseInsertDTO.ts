export default class SingleExpenseInsertDTO {
    public yearId: number;
    public monthId: number;
    public expenseTypeId: number;
    public expenseName: string;
    public expenseCost: number;

    constructor(yearID: string, monthID: string, expenseTypeID: string, expenseName: string, expenseCost: string) {
        this.yearId = parseInt(yearID);
        this.monthId = parseInt(monthID);
        this.expenseTypeId = parseInt(expenseTypeID);
        this.expenseName = expenseName;
        this.expenseCost = parseFloat(expenseCost);
    }
}