import React, { FC, useEffect, useState } from 'react';
import './GoalsCard.css';
import ComponentWrapper from '../../Components/Content-Wrapper/ContentWrapper.tsx'; // Adjust the import path as necessary
import moment from 'moment';
import { SemiCircleProgress } from 'react-semicircle-progressbar';
import { Button } from 'primereact/button';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';
import { Calendar } from 'primereact/calendar';
import { Nullable } from 'primereact/ts-helpers';


interface GoalsCardProps {
  isOverViewPage?: boolean;
  target: number;
  achieved: number;
  onUpdate: any;
  onMonthChange?: any;
}

const today = new Date();

const GoalsCard: FC<GoalsCardProps> = ({ isOverViewPage = true, target, achieved, onUpdate, onMonthChange }: GoalsCardProps) => {
  const [goalPercentage, setGoalPercentage] = useState(0);

  const [date, setDate] = useState<Nullable<Date>>(today);

  useEffect(() => {
    if (target === 0)
      setGoalPercentage(achieved === 0 ? 0 : 100);
    else {
      var percentage = (achieved * 100) / target;
      if (percentage > 100)
        percentage = 100;
      setGoalPercentage(parseFloat(percentage.toPrecision(3)));
    }
  }, [target, achieved]);

  const onUpdateClick = () => {

    onUpdate();
  }

  const updateDate = (value: Date) => {
    setDate(value);
    if (onMonthChange !== undefined)
      onMonthChange(value);
  }


  return (
    <div className='card flex-column gap-3'>
      {isOverViewPage &&
        <div className='d-flex justify-content-between card-head h-auto p-1 align-items-center border-bottom'>
          <div className='d-flex gap-2'>
            <span>{`₹ ${target}`}</span>
            <span onClick={() => { onUpdateClick() }} className='cursor-pointer'>
              <ComponentWrapper icon="pen-to-square"></ComponentWrapper>
            </span>
          </div>
          <span className='cursor-pointer fs-6 nav-date'>{moment(today).format("MMM, yyyy")}</span>
        </div>
      }

      {!isOverViewPage &&
        <div className='d-flex justify-content-between h-auto gap-3 py-2 align-items-center card-head border-bottom'>
          <div className='d-flex gap-2'>
            <span> Saving Goal</span>
          </div>
          <div id="datePicker" className='month-dropdown-container'>
            <Calendar inputClassName='month-picker-input' className='rounded-3' value={date} appendTo="self" onChange={(e) => e.value && updateDate(e.value)} variant='filled' view="month" dateFormat="M, yy" placeholder='Select a month' />
          </div>
        </div>
      }

      <div className='d-flex justify-content-between gap-1 rounded-2'>
        <div className='d-flex flex-column gap-4'>
          <div className='d-flex gap-2'>
            <i className="pi pi-arrow-circle-right mt-2" style={{ color: '#708090' }}></i>
            <div className='d-flex flex-column'>
              <span>Target Achieved</span>
              <span className='fw-bold fs-4 '>
                ₹{achieved}
              </span>
            </div>
          </div>
          <div className='d-flex gap-2'>
            <i className="pi pi-bullseye mt-2" style={{ color: '#708090' }}></i>
            <div className='d-flex flex-column'>
              <span>This Month Target</span>
              <span className='fw-bold fs-4 '>
                ₹{target}
              </span>
            </div>
          </div>
        </div>

        <div className='d-flex flex-column justify-content-between'>
          <div>
            <div className='text-end'>
              <SemiCircleProgress
                percentage={goalPercentage}
                size={{
                  width: 100,
                  height: 100,
                }}
                fontStyle={{
                  fontSize: '.875rem',
                  fontWeight: '500',
                  fill: '#000'
                }}
                percentageSeperator=''
                strokeWidth={10}
                strokeColor="#14b8a6"
                hasBackground={true}
                bgStrokeColor='#E8E8E8'
              />
            </div>
            {/* <div className='d-flex justify-content-between mt-2'>
              <span className=''>
                <i className="pi pi-indian-rupee" style={{ color: '#708090',  fontSize:'10px'}}></i>
                0
              </span>
              <span className=''>
                <i className="pi pi-indian-rupee" style={{ color: '#708090', fontSize:'10px' }}></i>
                {target}
              </span>
            </div> */}
          </div>
          <span>Target vs Achievement</span>
        </div>
      </div>
      {
        !isOverViewPage &&
        <div className='w-100 d-flex'>
          <Button label="Adjust Goal" icon="pi pi-pencil" className='rounded-3 m-auto' iconPos="right" onClick={() => { onUpdateClick() }} outlined />

        </div>
      }
    </div>
  );
}

export default GoalsCard;
