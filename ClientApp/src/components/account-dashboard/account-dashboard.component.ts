import { Component, Input, OnInit } from "@angular/core";
import * as d3 from 'd3'
import Transaction from "src/types/Transaction";
import TransactionType from "src/types/TransactionType";
import { FormControl, FormGroup, Validators } from "@angular/forms";

type TransactionGroupedByName = {
    name: string;
    total: number;
}

type TransactionData = {
    [key: number]: number;
}

type TransactionRecord = TransactionGroupedByName & TransactionData

@Component({
    selector: 'account-dashboard',
    templateUrl: './account-dashboard.component.html',
    styleUrls: ['./account-dashboard.component.css']
})
export class AccountDashboardComponent implements OnInit {
    @Input() transactions: Transaction[];
    data: TransactionRecord[] = [];
    public transactionForm = new FormGroup({
        type: new FormControl(0, [Validators.required])
    })

    prepareData = (data: Transaction[]): TransactionRecord[] => {
            
        const dashboardData: TransactionRecord[] = []
        return data.filter(t => t.transactionType === Number(this.transactionForm.value.type)).reduce((acc, el) => {
            const date = el.date.split('T')[0];
            const doesExist = acc.some(t => t.name === date);
            let categoryId = 0; // think of symbol key
                        if(el.category !== null) {
                            categoryId = el.category.id;
            }
            // have to distinguish income / expense
            if(doesExist){
                return acc.map(t => {
                    
                    if(t.name === date){
                        const val = Math.abs(el.total);
                        
                        const currentValue = t[categoryId]
                        
                        t[categoryId] = currentValue === undefined 
                        ? val
                        : currentValue + val;

                        t.total = t.total + val;
                        
                    }
                    return t;
                })
            } else {

                const newRecord: TransactionRecord = {
                    name: date,
                    [categoryId]: Math.abs(el.total),
                    total: Math.abs(el.total)
                };

                return [...acc, newRecord];
            }

        }, dashboardData);
    } 

    onChanges(): void {
        this.transactionForm.valueChanges.subscribe(val => {
            this.data = (this.prepareData(this.transactions));
            d3.select('#dashboard').select('svg').remove();
            this.createDashboard();
        });
    }

    createDashboard() {
        this.data = (this.prepareData(this.transactions));
        
        const margin = {top: 20, right: 0, bottom: 30, left: 40};
        const height = 200;
        const width = 500;

        const x = d3.scaleBand()
        .domain(d3.range(this.data.length))
        .range([margin.left, width - margin.right])
        .padding(0.1);

        const y = d3.scaleLinear()
        .domain([0, d3.max(this.data, d => d.total)]).nice()
        .range([height - margin.bottom, margin.top])

        const xAxis = g => g
        .attr("transform", `translate(0,${height - margin.bottom})`)
        .call(d3.axisBottom(x).tickFormat(i => this.data[i].name).tickSizeOuter(0));
        
        const yAxis = g => g
        .attr("transform", `translate(${margin.left},0)`)
        .call(d3.axisLeft(y))
        .call(g => g.select(".domain").remove())
        
        const chart = () => {
            const svg = d3.select("#dashboard").append("svg")
                .attr("viewBox", [0, 0, width, height]);
          
            svg.append("g")
                .attr("fill", "steelblue")
              .selectAll("rect")
              .data(this.data)
              .join("rect")
                .attr("x", (d, i) => x(i))
                .attr("y", d => y(d.total))
                .attr("height", d => y(0) - y(d.total))
                .attr("width", x.bandwidth());
          
            svg.append("g")
                .call(xAxis);
          
            svg.append("g")
                .call(yAxis);
          
          }

          chart();
    }

    ngOnInit() {
        this.createDashboard();
        this.onChanges();
     }
}