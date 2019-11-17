﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase {
        // GET api/values/5/5
        [HttpGet("{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber) {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid imput");
        }

        private decimal ConvertToDecimal(string Number) {
            decimal decimalValue;
            if (decimal.TryParse(Number, out decimalValue)) {
                return decimalValue;
            }
            return 0;
        }

        private bool IsNumeric(string sNumber) {
            double number;
            bool isNumber = double.TryParse(sNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isNumber;
        }
    }
}

