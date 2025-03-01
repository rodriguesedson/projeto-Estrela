import { Calendar } from "@heroui/react";
import { I18nProvider } from "@react-aria/i18n";

export default function PageCalendar({data, setData, setAulasDoDia}) {
  return (
    <div className="flex flex-col justify-center items-center gap-8">
      <I18nProvider locale="pt-BR">
        <Calendar
          showMonthAndYearPickers
          aria-label="Date (Show Month and Year Picker)"
          focusedValue={data}
          nextButtonProps={{
            variant: "bordered",
          }}
          prevButtonProps={{
            variant: "bordered",
          }}
          value={data}
          onChange={() => {
            setData();
            setAulasDoDia([]);
          }}
          onFocusChange={setData}
          classNames={{
            nextButton:"bg-white w-20 h-10",
            prevButton:"bg-white w-20 h-10",
            headerWrapper:"bg-lightbrown",
            header:"bg-lightbrown text-white border-none",
            title:"text-white text-[1.1rem] relative title",
            gridHeader:"bg-lightbrown",
            gridHeaderRow:"text-white",
            gridHeaderCell:"w-[30px] h-[30px]",
            cell:"w-[30px] h-[40px]",
            cellButton:"w-[40px] h-[40px]",
          }}
        />
      </I18nProvider>
    </div>
  )
}