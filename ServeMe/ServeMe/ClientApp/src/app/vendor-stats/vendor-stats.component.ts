import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vendor-stats',
  templateUrl: './vendor-stats.component.html',
  styleUrls: ['./vendor-stats.component.css']
})
export class VendorStatsComponent implements OnInit {
  basicData = {
    labels: ['2 Aug', '3 Aug', '4 Aug', '5 Aug', '6 Aug', '7 Aug', '8 Aug', '9 Aug'],
    datasets: [
      {
        label: 'Earning',
        backgroundColor: '#42A5F5',
        data: [65, 59, 80, 81, 56, 55, 40]
      }
    ]
  };
  basicOptions = {
    plugins: {
      legend: {
        labels: {
          color: '#000'
        }
      }
    },
    scales: {
      x: {
        ticks: {
          color: '#000'
        },
        grid: {
          color: 'rgba(255,255,255,0.2)'
        }
      },
      y: {
        ticks: {
          color: '#000'
        },
        grid: {
          color: 'rgba(255,255,255,0.2)'
        }
      }
    }
  };
  constructor() { }

  ngOnInit(): void {
  }

}
