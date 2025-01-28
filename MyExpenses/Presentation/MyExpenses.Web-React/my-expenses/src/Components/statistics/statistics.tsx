import React, { Children, FC, useEffect, useState } from 'react';
import './statistics.module.css';
import { Chart } from 'primereact/chart';
import { getByWeeklyExpenses } from '../../Services/transactionService.tsx';

interface statisticsProps {
  type?: string,
  userId: number
}

const Statistics: FC<statisticsProps> = ({ type = 'weekly', userId }: statisticsProps) => {
  const [chartData, setChartData] = useState({});
  const [chartOptions, setChartOptions] = useState({});
  const [currentExpenses, setCurrentExpense] = useState<number[]>([]);
  const [previousExpense, setPreviousExpense] = useState<number[]>([]);


  const documentStyle = getComputedStyle(document.documentElement);
  const textColor = documentStyle.getPropertyValue('--sub-text-color');
  const textColorSecondary = documentStyle.getPropertyValue('--secondary-text-color');
  const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

  useEffect(() => {
    // Fetch expenses for current and previous week
    const fetchExpenses = async () => {
      try {
        const currentResponse = await getByWeeklyExpenses(userId, true);
        setCurrentExpense(currentResponse.data.expenses);

        const previousResponse = await getByWeeklyExpenses(userId, false);
        setPreviousExpense(previousResponse.data.expenses);
      } catch (error) {
        console.error("Error fetching expenses:", error);
      }
    };

    fetchExpenses();
  }, [userId]);

  // Update chart data whenever current or previous expenses change
  useEffect(() => {
    if (currentExpenses.length > 0 && previousExpense.length > 0) {
      const data = {
        labels: getLabels(),
        datasets: [
          {
            label: 'Last Week',
            backgroundColor: '#9F9F9F',
            borderColor: '#9F9F9F',
            data: previousExpense,
          },
          {
            label: 'Current Week',
            backgroundColor: '#14b8a6',
            borderColor: '#14b8a6',
            data: currentExpenses,
          },
        ],
      };

      const options = {
        maintainAspectRatio: false,
        aspectRatio: .9,
        plugins: {
          legend: {
            labels: {
              fontColor: textColor,
            },
          },
        },
        scales: {
          x: {
            ticks: {
              color: textColorSecondary,
              font: {
                weight: 400,
              },
            },
            grid: {
              display: false,
              drawBorder: true,
            },
          },
          y: {
            ticks: {
              color: textColorSecondary,
            },
            grid: {
              color: surfaceBorder,
              drawBorder: true,
            },
          },
        },
      };

      setChartData(data);
      setChartOptions(options);
    }
  }, [currentExpenses, previousExpense]);

  const getLabels = () => {
    var day = new Date().getDay();
    if (type === 'weekly') {
      var arr = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
      return arr.slice(day).concat(arr.slice(0, day));
    }
    else {
      return [];
    }
  }
  return (
    <div className='card'>
      <Chart type="bar" data={chartData} options={chartOptions} />
    </div>
  );
}

export default Statistics;
