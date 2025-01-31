import React, { FC, useContext, useEffect, useState } from 'react';
import './TargetForm.module.css';
import { InputNumber, InputNumberChangeEvent } from 'primereact/inputnumber';
import { InputText } from 'primereact/inputtext';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';
import { Goal, UpdateGoal } from '../../Models/Goals';
import { Button } from 'primereact/button';
import { updateGoal } from '../../Services/categoryService';
import UserContext from '../../Context/UserContext.ts';

interface TargetFormProps {
  amount?: number;
  categories:Category[];
  category?: string;
  date?:Date;
  goals: Goal[],
  onSubmit: (data: { updateGoal: UpdateGoal }) => void;
}export interface Category {
  categoryName: string;
  id: number;
}

const TargetForm: FC<TargetFormProps> = ({ amount = 0, category, goals, date, categories, onSubmit }: TargetFormProps) => {
  const [targetAmount, setTargetAmount] = useState<number>(amount);
  const [currentTargetAmount, setCurrentTargetAmount] = useState<number>(0);
  const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);
  const [selectedGoal, setSelectedGoal] = useState<Goal | null>(null);
  const { user } = useContext(UserContext);

  useEffect(() => {
    const goal = goals.find(g => g.categoryName === category);
    setSelectedGoal(goal ?? null);
    setTargetAmount(goal?.budget ?? 0);
    // console.log("logged from target form",goals)
  }, [category]);

  // useEffect(() => {
  //   const goal = goals.find(g => g.categoryName === selectedCategory?.name);
  //   console.log(goal)
  //   setSelectedGoal(goal ?? null);
  // }, [selectedGoal, selectedCategory]);

  const syncCurrentTargetAmount = (amount: number) => {
    setCurrentTargetAmount(amount);
  }

  const handleSubmit = () => {
    const updatedGoal: UpdateGoal = {
      userId: user.id,
      id: selectedGoal?.id ?? 0,
      budget: currentTargetAmount,
      month: (date ? date.getMonth() + 1 : new Date().getMonth() + 1),
      year: (date ? date.getFullYear() : new Date().getFullYear()),
      categoryId: selectedCategory?.id ?? 6
    };
    onSubmit({ updateGoal: updatedGoal });
  }

  return (
    <div className=''>
      {category !== undefined ?
        (
          <div className='mb-3 mt-2'>
            <div className="flex flex-column gap-2">
              <label htmlFor="category" className='text-black fw-medium'>Category</label>
              <InputText id="category" className='w-100' value={category} disabled={true} />
            </div>
          </div>
        ) : (
          <div className='mb-3 mt-2 '>
            <label htmlFor="currentTargetAmount" className='text-black fw-medium'>Category</label>
            <Dropdown value={selectedCategory} focusOnHover={false} panelClassName='category-dropdown' onChange={(e: DropdownChangeEvent) => setSelectedCategory(e.value)} options={categories} optionLabel="categoryName"
              placeholder="Select a Category" className="w-100 md:w-14rem" />
          </div>
        )
      }
      <div className='mb-3 mt-2'>
        <div className="flex flex-column gap-2">
          <label htmlFor="target" className='text-black fw-medium'>Target Amount</label>
          <InputNumber id="target" className='w-100' value={targetAmount} prefix="₹ " disabled={true} />
        </div>
      </div>
      <div className='mb-3 mt-2'>
        <div className="flex flex-column gap-2">
          <label htmlFor="currentTargetAmount" className='text-black fw-medium'>Current Target Amount</label>
          <InputNumber id="currentTargetAmount" className='w-100' value={currentTargetAmount} minFractionDigits={2} onChange={(e: InputNumberChangeEvent) => syncCurrentTargetAmount(e.value ?? 0)} mode="currency" currency="INR" currencyDisplay="code" locale="en-IN" />
        </div>
      </div>
      <div className="d-flex justify-content-center">
        <Button label='Submit' className='rounded-3' onClick={handleSubmit} />
      </div>

    </div>
  );
}

export default TargetForm;
