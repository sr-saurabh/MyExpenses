import React, { FC, useEffect, useState } from 'react';
import './ContentWrapper.css';

interface ContentWrapperProps {
  icon?: string | null;
  children?: React.ReactNode | null
  iconColor?: string,
  size?: string,
  height?: string,
  width?: string,
  parentClass?:string
}

const ContentWrapper: FC<ContentWrapperProps> = ({ icon, children, iconColor, size, height, width, parentClass }: ContentWrapperProps) => {
  const [containerHeight, setHeight] = useState('');
  const [containerWidth, setWidth] = useState('');

  useEffect(() => {

    if (!!height && !!width) {
      setHeight(height);
      setWidth(width);
    }
    else if (!!size) {
      switch (size) {
        case 'regular':
          setHeight('32px');
          setWidth('32px');
          break;
        case 'large':
          setHeight('40px');
          setWidth('40px');
          break;
        default:
          setHeight('32px');
          setWidth('32px');
      }
    }
    else {
      setHeight('32px');
      setWidth('32px');
    }
  }, [size, height, width]);
  return (
    <div className={`wrapper-content rounded-3 d-flex justify-content-center align-items-center ${parentClass}`} style={{ height: `${containerHeight}`, width: `${containerWidth}` }}>
      {!!icon &&
        <div>
          <i className={`pi pi-${icon}`} style={{ color: `${!!iconColor ? iconColor : '#708090'}` }}></i>
        </div>
      }

      {!!children &&
        <div>
          {children}
        </div>
      }

    </div>
  );
};

export default ContentWrapper;
