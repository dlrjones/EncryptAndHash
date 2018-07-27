using System;

namespace MPOUS_ItemsOnOrder
{
	/// <summary>
	/// Summary description for DateTimeUtilities.
	/// </summary>
	public class DateTimeUtilities
	{
		public DateTimeUtilities()
		{}

		public string DayOfTheWeek() {
			return (DateTime.Now).DayOfWeek.ToString();
		}

		public string DateFromNow(DateTime dt,object daysFromNow) {	
			//	daysFromNow is an integer number of days which can be negative.
			//  returns ShortDate format M/d/yyyy 
			string newDate = "";
			try {
				newDate = dt.Add(new TimeSpan(Convert.ToInt32(daysFromNow),0,0,0,0)).ToString("d");
			}catch(InvalidCastException) {			
			}catch(FormatException){}
			return newDate;
		}

		/// <summary>
		/// [< 0 : dt1 < dateObj]
		/// [= 0 : dt1 = dateObj]
		/// [> 0 : dt1 > dateObj]
		/// </summary>
        /// <param name="dateObj">an unknown object which may or may not be a Date</param>
        /// <param name="dt1">a future, present, or past date</param>
        /// <param name="compValu">the result of the Compare</param>
        /// <returns>a boolean to indicate the success of the Compare</returns>
		public bool CompareDates(object dateObj, DateTime dt1, ref int compValu) {
			//dateObj is an unknown object which may or may not be a Date
			//dt1 is a future, present, or past date
			//compValu is the result of the Compare [< 0 : dt1 < dateObj]  [= 0 : dt1 = dateObj]  [> 0 : dt1 > dateObj]
			// the '<', '>' and '=' refer to relative position on the timeline.
			//returns: a boolean to indicate the success of the Compare.
			//		   compValu is a ref that has the actual comparison value
			bool rtnValu = false;
			try {
				compValu = dt1.CompareTo(Convert.ToDateTime(dateObj));
				rtnValu = true;
			}catch (ArgumentException) {			
			}catch(FormatException) {
			}catch(Exception){}
			return rtnValu;
		}

		public string ParseLongDate(string dt) 
		{ //converts a LongDateString [Day,Month Date,Year]
		  //to [Month Date,Year]
			char[] delimiter = " ".ToCharArray();
			string[] splitStr = dt.Split(delimiter);
			dt = splitStr[1] + " " + splitStr[2] + " " + splitStr[3];
			return dt;
		}

		public string DateTimeToShortDate(DateTime dt) {		
			//returns ShortDate format M/d/yyyy
			return dt.ToString("d");			 
		}

		public string DateTimeToShortDate(DateTime dt, int daysFromNow ) {	
			//depricated. Use:
			return DateFromNow(dt,daysFromNow);
		}

		public string DateTimeObjectToShortDateString(object dtObject) {
			string dt = dtObject.ToString();//cast not valid if object doesn't implement the IConvertible interface
			return DateTimeToShortDate(Convert.ToDateTime(dt));
		}

		public string DateTimeStringToShortDate(string dt) {			
			char[] delimiter = " ".ToCharArray();
			int x = 0;
			string[] splitStr = dt.Split(delimiter);
			delimiter = "/".ToCharArray();
			for(x =0; x < splitStr.Length; x++) {
				if(splitStr[x].IndexOfAny(delimiter) > 0)
					break;
			}
			return splitStr[x].ToString();
		}

		public string TimeOfDayCoded() 
		{
			//using a 24 hour clock, returns [hours min sec millisecs] as a string
			//with no colons or periods
			string[]tod;
			string delimitString = " :.";
			char [] delimiter = delimitString.ToCharArray();
			tod = DateTime.Now.TimeOfDay.ToString().Split(delimiter);
			return tod[0]+tod[1]+tod[2]+tod[3];
		}

        /// <summary>
        /// Calculates the number of days seperating two dates
        /// </summary>
        /// <param name="dt1">date 1 [form: mm/dd/yyyy]</param>
        /// <param name="dt2">date 2 [form: mm/dd/yyyy]</param>
        /// <returns>The number of days between the two dates</returns>
		public int DaysBetweenTwoDates(string dt1, string dt2) {
			//dt1 & dt2 in the form: mm/dd/yyyy
			//handles multi-year gaps
			int tempValu = 0;
			int ytd1 = 0;
			int ytd2 = 0;
			int m1 = 0;
			int m2 = 0;
			int d1 = 0;
			int d2 = 0;
			int y1 = 0;
			int y2 = 0;			
			char[] delimiter = "/".ToCharArray();
			string[]d1BreakDown = dt1.Split(delimiter);
			string[]d2BreakDown = dt2.Split(delimiter);

			//date 1 (today?)
			m1 = Convert.ToInt32(d1BreakDown[0]) ;
			d1 = Convert.ToInt32(d1BreakDown[1]);
			y1 = Convert.ToInt32(d1BreakDown[2]);

			//date 2 (future day?)
			m2 = Convert.ToInt32(d2BreakDown[0]);
			d2 = Convert.ToInt32(d2BreakDown[1]);
			y2 = Convert.ToInt32(d2BreakDown[2]);
			
			if(y1 > y2){//swap all of the numbers so that the earliest date is in m1-d1-y1
				tempValu = m1;
				m1 = m2;
				m2 = tempValu;

				tempValu = d1;
				d1 = d2;
				d2 = tempValu;

				tempValu = y1;
				y1 = y2;
				y2 = tempValu;
			}
			//calculate the number of days in all of the years between (y1+1) and y2
			//the remainder of the first year is calculated as usual below - see: if(y1 < y2)...
			int yearGap = y2 - y1;	
			int ytdGap = 0;
			if(yearGap > 1){				
				for(int year = y1 + 1;  year < y2 ; year++) {
					for(int month = 1;month <= 12; month++){
						ytdGap += DateTime.DaysInMonth(year, month);
					}
				}			
			}
			//this will always be the larger of the two dates (as determined by the year value)
			//so get the day count from the beginning of the year to the target date (m2/d2)
			for(int x = 1; x < m2; x++){
				ytd2 += DateTime.DaysInMonth(y2, x);
			}
			ytd2 += d2;
			if(y1 == y2) {//same year so count y1 values from beginning of that year to the target date (m1/d1)
				//then subtract the ytd2 value.
				for(int x = 1; x < m1; x++){
					ytd1 += DateTime.DaysInMonth(y1, x);
				}
				ytd1 += d1;
				ytd1 = ytd1 - ytd2;
			}else if(y1 < y2){	//11/15/2003 is less than 1/15/2006 so count the 2003 days from the  
				//end of the year back to the target date then add the ytd2 values and the intervening 
				//days of each year (ytdGap) if any.
				int DIM = DateTime.DaysInMonth(y1, m1);
				for(int x = 12; x > m1; x--){
					ytd1 += DateTime.DaysInMonth(y1, x);
				}
				ytd1 += (DIM - d1);
				ytd1 = ytd1 + ytd2 + ytdGap;
			}			
			return (ytd1 < 0) ? (ytd1 *= -1) : ytd1;
		}
       
        /// <summary>
        /// Returns a hash of DateTime.Now including milliseconds
        /// </summary>
        /// <param name="[no params]"></param>
        /// <returns> DateTime.Now as a hash code</returns>
		public string DateTimeHash() {
			//returns the hash of [mmddyyhhmmssxxxxxxx] as a string
			string dtCoded = DateTimeCoded(true);
			object dtHash = dtCoded.GetHashCode();
			dtHash = dtHash.ToString().GetHashCode();
			return dtHash.ToString();
		}

        /// <summary>
        /// Returns DateTime.Now as a short datetime in the form ddmmyyhhmmss
        /// </summary>
        /// <param name="[no params]"></param>
        /// <returns>DateTime.Now as ddmmyyhhmmss</returns>
        public string DateTimeCoded()
        {
            return DateTimeCoded(false);
        }
        /// <summary>
        /// Returns a short datetime in the form ddmmyyhhmmss[xxxxxxx]
        /// </summary>
        /// <param name="withMilliSeconds">True=Include milliseconds</param>
        /// <returns> DateTime.Now as ddmmyy</returns>
		public string DateTimeCoded(bool withMilliSeconds) {
			//returns [month day year hours min sec] as a string 
			//withMilliSeconds = true appends the 7 millisecond chars to the end [mmddyyhhmmssxxxxxxx]
			string rtnValu = "";
			string[] date;
			string[] tod;
			string delimitString = " :./";
			char [] delimiter = delimitString.ToCharArray();

			date = DateTime.Now.ToString().Split((delimiter));
			tod = DateTime.Now.TimeOfDay.ToString().Split((delimiter));
			for(int x = 0; x < date.Length - 1; x++) {//the date[] has 7 values the last being "AM" or "PM", which we don't want
				if(date[x].Length == 4) {//a four digit value is the year
					char[] year = date[x].ToCharArray();
					rtnValu += year[2].ToString();//for a four digit year, strip off
					rtnValu += year[3].ToString();//the last two digits
				}else{
					if(date[x].Length == 0)//prepend leading zero's
						rtnValu += "00";
					if(date[x].Length == 1)
						rtnValu += "0";
					rtnValu += date[x];
				}			
			}
			if(withMilliSeconds) {
				rtnValu += tod[3].ToString(); //the tod[] has 4 values (h-m-s-ms) so element 3 holds the millisecond
			}

			return rtnValu;
		}

        /// <summary>
        /// Returns DateTime.Now as a short date in the form ddmmyy
        /// </summary>
        /// <param name="[no params]"></param>
        /// <returns>DateTime.Now as ddmmyy</returns>
        public string ShortDateEncoded()
        {
            return ShortDateEncoded(DateTime.Now);
        }
        /// <summary>
        /// Provides a short date in the form ddmmyy
        /// </summary>
        /// <param name="thisDT">The DateTime value you're interested in</param>
        /// <returns> the given DateTime as ddmmyy</returns>
        public string ShortDateEncoded(DateTime thisDT)
        {
            string rtnValu = "";
            string[] sde = DateTimeToShortDate(thisDT).Split("/".ToCharArray());
            rtnValu = sde[0].Length < 2 ? "0" + sde[0] : sde[0];
            rtnValu += sde[1].Length < 2 ? "0" + sde[1] : sde[1];
            rtnValu += sde[2].Length == 4 ? sde[2].Substring(2, 2) : sde[2];
            return rtnValu;
        }

        public string GetDate()
        {
            //returns format - {5/30/2013 12:24:04 PM}
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            return dt.ToString();
        }

		public string ConvertToLongDate(string shortDate) {
			//shortDate is in the form mm/dd/yyyy (or mm/dd/yy) (or m/d/yy) etc.
			string newDate = "";
			string[] dateSplit = shortDate.Split("/".ToCharArray());
			if(dateSplit[0].StartsWith("0"))
				dateSplit[0] = dateSplit[0].Remove(0,1);
			if(dateSplit[1].StartsWith("0"))
				dateSplit[1] = dateSplit[1].Remove(0,1);

		    newDate = GetMonthName(Convert.ToDateTime(shortDate));
		    
			newDate += dateSplit[1] + ", ";	//get the date of the month

			if(dateSplit[2].Length == 2) {	//get the year 00 to 79 become 2000 to 2079  years 80 to 99 become 1980 to 1999
				if(Convert.ToInt32(dateSplit[2])  > 79 )
					newDate += "19" + dateSplit[2];
				else
					newDate += "20" + dateSplit[2];
			}else
				newDate += dateSplit[2];
			
			return newDate;
		}

	    public string GetMonthName(int offset)
	    {
	        return GetMonthName(DateTime.Now, offset);
	    }

        public string GetMonthName(DateTime dt)
        {
            return GetMonthName(dt, 0);
        }

        public string GetMonthName(string newDate)
        {
            return GetMonthName(Convert.ToDateTime(newDate), 0);
        }

        public string GetMonthName(string newDate, int offset)
        {
            return GetMonthName(Convert.ToDateTime(newDate), offset);
        }

		public string GetMonthName(DateTime dt, int offset)
        {//returns the name of the current month. A positive offset value will give the month name in the future (currentMonth + offset).
		  //offset can be negative for preceeding months
            string monthName = "";
            //DateTime dt = new DateTime();
            //dt = DateTime.Now;
             DateTime month = dt.AddMonths(offset);
             switch (month.Month)
            {
                case 1:
                    monthName = "January";
                    break;
                case 2:
                    monthName = "February";
                    break;
                case 3:
                    monthName = "March";
                    break;
                case 4:
                    monthName = "April";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "June";
                    break;
                case 7:
                    monthName = "July";
                    break;
                case 8:
                    monthName = "August";
                    break;
                case 9:
                    monthName = "September";
                    break;
                case 10:
                    monthName = "October";
                    break;
                case 11:
                    monthName = "November";
                    break;
                case 12:
                    monthName = "December";
                    break;
            }
            return monthName;
        }
	}
}
