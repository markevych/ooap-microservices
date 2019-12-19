import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface Car {
    vin;
    year;
    brand;
    color;
  }

@Injectable({
    providedIn: 'root'
})
export class CarService {
    private data = [
        {"Student": "Klymenko", "12/18": "4", "12/10": "4", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "Haihel", "12/18": "5", "12/10": "-", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "Markevych", "12/18": "4", "12/10": "-", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "test", "12/18": "-", "12/10": "3", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "test", "12/18": "4", "12/10": "-", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "test", "12/18": "-", "12/10": "5", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "tset", "12/18": "3", "12/10": "2", "12/2": "4", "11/25": "4", "11/10": "4" },
        {"Student": "test", "12/18": "2", "12/10": "-", "12/2": "4", "11/25": "4", "11/10": "4" }
    ]

    constructor() {}

    public getCarsSmall(): any {
        return this.data;
    }

    public buildTestData(date: Date): any {
        let dateD = [];
        let amountOfDays = date.getDate();
        for (var i = 0; i < amountOfDays; i++) {
            dateD.push({"Math": (i + 5).toString(), "asfasf": i, "afsfasf": i + 1, "fasfasfa": i*2, "fasxzvxz": i + i})
        }
        return dateD;
    }
}
