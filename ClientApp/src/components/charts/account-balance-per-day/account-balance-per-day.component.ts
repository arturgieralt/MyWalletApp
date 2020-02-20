import { Component, Input, OnInit, SimpleChanges } from "@angular/core";
import * as d3 from 'd3'
import Transaction from "src/types/Transaction";

type AccountBalancePerDay = {
    date: string;
    value: number;
}

@Component({
    selector: 'account-balance-per-day-chart',
    templateUrl: './account-balance-per-day.component.html',
    styleUrls: ['./account-balance-per-day.component.css']
})
export class AccountBalancePerDayChart implements OnInit {
    @Input() transactions: Transaction[];
    data: AccountBalancePerDay[] = [];

    prepareData = (data: Transaction[]): AccountBalancePerDay[] => {
            
        const dashboardData: AccountBalancePerDay[] = []
        return data
            .sort((a, b) => {
                const aDate = new Date(a.date)
                const bDate = new Date(b.date)
                return aDate.getTime() - bDate.getTime();
            })
            .reduce((acc, el) => {
    
    
            const date = el.date.split('T')[0];
            const doesExist = acc.some(t => t.date === date);

            if(doesExist){
                return acc.map(t => {
                    
                    if(t.date === date){
                        t.value = t.value + el.total;
                        
                    }
                    return t;
                })
            } else {

                const prevDayValue = acc.length === 0 
                    ? 0
                    : acc[acc.length -1].value 
                const newRecord: AccountBalancePerDay = {
                    date: date,
                    value: prevDayValue + el.total
                };

                return [...acc, newRecord];
            }

        }, dashboardData);
    } 

    createDashboard() {
        this.data = (this.prepareData(this.transactions));
        var margin = {top: 20, right: 20, bottom: 50, left: 70};
        var width = 500 - margin.left - margin.right;
        var height = 200 - margin.top - margin.bottom;
        
        //add svg with margin !important
        //this is svg is actually group
        var svg = d3.select("#account-balance-per-day").append("svg")
                    .attr("width",width+margin.left+margin.right)
                    .attr("height",height+margin.top+margin.bottom)
                    .append("g")  //add group to leave margin for axis
                    .attr("transform","translate("+margin.left+","+margin.top+")");
        
        const values = this.data.map(b => b.value);
        const keys = this.data.map(b => b.date);
        var maxHeight=d3.max(values,function(d){return Math.abs(d)});
        var minHeight=d3.min(values,function(d){return Math.abs(d)})
        
        //set y scale
        var yScale = d3.scaleLinear().rangeRound([0,height]).domain([maxHeight,-maxHeight]);//show negative
        //add x axis
        var xScale = d3.scaleBand().rangeRound([0,width]).padding(0.1);//scaleBand is used for  bar chart
        xScale.domain(keys);//value in this array must be unique

        var yAxis = d3.axisLeft(yScale);
	    svg.append("g").call(yAxis);
    
        	//add label for x axis and y axis

	    var xAxis = d3.axisBottom(xScale);/*.tickFormat("");remove tick label*/
	    svg.append("g").call(xAxis).attr("transform", "translate(0,"+height/2+")");

        const line = d3.line()
        .defined(d => !isNaN(d.value))
        .x(d => xScale(d.date))
        .y(d => yScale(d.value));

  
    svg.append("path")
        .datum(this.data)
        .attr("fill", "none")
        .attr("stroke", "steelblue")
        .attr("stroke-width", 1.5)
        .attr("stroke-linejoin", "round")
        .attr("stroke-linecap", "round")
        .attr("d", line);
    

}

    ngOnInit() {
        this.createDashboard();
    }
}