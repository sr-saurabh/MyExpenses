import React, { FC, useEffect, useState } from 'react';
import './GoalsCard.module.css';
import ComponentWrapper from '../../Components/Content-Wrapper/ContentWrapper.tsx'; // Adjust the import path as necessary
import moment from 'moment';
import { SemiCircleProgress } from 'react-semicircle-progressbar';


interface GoalsCardProps {
  isOverViewPage?: boolean;
  target: number;
  achieved: number;
  onUpdate: any;
}

const today = new Date();

const GoalsCard: FC<GoalsCardProps> = ({ isOverViewPage = true, target, achieved, onUpdate }: GoalsCardProps) => {
  const [goalPercentage, setGoalPercentage] = useState(0);
  useEffect(() => {
    if (target === 0)
      setGoalPercentage(achieved === 0 ? 0 : 100);
    else {
      var percentage = (achieved * 100) / target;
      setGoalPercentage(parseFloat(percentage.toPrecision(3)));
    }
  }, [target, achieved]);

  const onUpdateClick = () => {
    onUpdate();
  }
  return (
    <div className='card flex-column gap-3'>
      {isOverViewPage &&
        <div className='d-flex justify-content-between card-head border-bottom'>
          <div className='d-flex gap-2'>
            <span>{`Rs. ${target}`}</span>
            <span onClick={() => { onUpdateClick() }} className='cursor-pointer'>
              <ComponentWrapper icon="pen-to-square"></ComponentWrapper>
            </span>
          </div>
          <span className='cursor-pointer fs-6 nav-date'>{moment(today).format("MMM, yyyy")}</span>
        </div>
      }

      {!isOverViewPage &&
        <div className='d-flex justify-content-between card-head border-bottom'>
          <div className='d-flex gap-2'>
            <span>{`$24399`}</span>
            <ComponentWrapper icon="pen-to-square"></ComponentWrapper>
          </div>
          <span className='cursor-pointer fs-6 nav-date'>{moment(today).format("MMM, yyyy")}</span>
        </div>
      }

      <div className='d-flex justify-content-between gap-1 rounded-2'>
        <div className='d-flex flex-column gap-4'>
          <div className='d-flex gap-2'>
            <i className="pi pi-arrow-circle-right mt-2" style={{ color: '#708090' }}></i>
            <div className='d-flex flex-column'>
              <span>Target Achieved</span>
              <span className='fw-bold fs-4 '>
                Rs.{achieved}
              </span>
            </div>
          </div>
          <div className='d-flex gap-2'>
            <i className="pi pi-arrow-circle-right mt-2" style={{ color: '#708090' }}></i>
            <div className='d-flex flex-column'>
              <span>This Month Target</span>
              <span className='fw-bold fs-4 '>
                Rs.{target}
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
        <div>
          Adjust Goal
        </div>
      }
    </div>
  );
}

export default GoalsCard;
