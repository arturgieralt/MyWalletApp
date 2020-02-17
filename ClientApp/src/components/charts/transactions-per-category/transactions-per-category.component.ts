import { Component, Input, OnInit, SimpleChanges } from "@angular/core";
import * as d3 from 'd3'
import Transaction from "src/types/Transaction";
import TransactionType from "src/types/TransactionType";

type TransactionGroupedByCategory = {
    id: number;
    name: string;
    total: number;
}

@Component({
    selector: 'transactions-per-category-chart',
    templateUrl: './transactions-per-category.component.html',
    styleUrls: ['./transactions-per-category.component.css']
})
export class TransactionsPerCategoryChart implements OnInit {
    @Input() transactions: Transaction[];
    @Input() type: TransactionType;
    data: TransactionGroupedByCategory[] = [];

    prepareData = (data: Transaction[]): TransactionGroupedByCategory[] => {
            
        const dashboardData: TransactionGroupedByCategory[] = []
        return data
            .filter(t => t.transactionType === Number(this.type))
            .reduce((acc, el) => {
            let categoryId = 0; // think of symbol key
                        if(el.category !== null) {
                            categoryId = el.category.id;
            }

            const doesExist = acc.some(t => t.id === categoryId);

            if(doesExist){
                return acc.map(t => {
                    
                    if(t.id === categoryId){
                        const val = Math.abs(el.total);
                        t.total = t.total + val;
                        
                    }
                    return t;
                })
            } else {

                const newRecord: TransactionGroupedByCategory = {
                    id: categoryId,
                    name: categoryId === 0 ? 'NoCategory' : el.category.name,
                    total: Math.abs(el.total)
                };

                return [...acc, newRecord];
            }

        }, dashboardData);
    } 

    createDashboard() {
        this.data = (this.prepareData(this.transactions));

        const width = 500;
        const height = Math.min(width, 300);

        const pie = d3.pie()
        .sort(null)
        .value(d => d.total);

        const arcLabel = () => {
            const radius = Math.min(width, height) / 2 * 0.8;
            return d3.arc().innerRadius(radius).outerRadius(radius);
        }

        const color = d3.scaleOrdinal()
        .domain(this.data.map(d => d.name))
        .range(d3.quantize(t => d3.interpolateSpectral(t * 0.5 + 0.1), this.data.length).reverse())

        const arc = d3.arc()
            .innerRadius(0)
            .outerRadius(Math.min(width, height) / 2 - 1);

        const chart = () => {
                const arcs = pie(this.data);
              
                const svg = d3.select('#transactions-per-category').append("svg")
                    .attr("viewBox", [-width / 2, -height / 2, width, height]);
              
                svg.append("g")
                    .attr("stroke", "white")
                  .selectAll("path")
                  .data(arcs)
                  .join("path")
                    .attr("fill", d => color(d.data.name))
                    .attr("d", arc)
                  .append("title")
                    .text(d => `${d.data.name}: ${d.data.total.toLocaleString()}`);
              
                svg.append("g")
                    .attr("font-family", "sans-serif")
                    .attr("font-size", 12)
                    .attr("text-anchor", "middle")
                  .selectAll("text")
                  .data(arcs)
                  .join("text")
                    .attr("transform", d => `translate(${arcLabel().centroid(d)})`)
                    .call(text => text.append("tspan")
                        .attr("y", "-0.4em")
                        .attr("font-weight", "bold")
                        .text(d => d.data.name))
                    .call(text => text.filter(d => (d.endAngle - d.startAngle) > 0.25).append("tspan")
                        .attr("x", 0)
                        .attr("y", "0.7em")
                        .attr("fill-opacity", 0.7)
                        .text(d => d.data.total.toLocaleString()));
              
              }

              chart();
    }

    ngOnInit() {
        this.createDashboard();
    }

    ngOnChanges(changes: SimpleChanges) {

        const typeChange = changes['type'];
        const shouldRedraw = typeChange.previousValue !== typeChange.currentValue && !(typeChange.firstChange);
        if(shouldRedraw) {
            d3.select('#transactions-per-category').select('svg').remove();
            this.createDashboard();
        }
      }
}