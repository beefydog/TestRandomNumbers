﻿
$(document).ready(function () {
    $('#btnget').click(function () {
        GetData(formToJson());
    });

    // for colors array - use different colors for each number group
    let grp1count = 0;
    let grp2count = 0;
    let grp3count = 0;
    let colorsAry = [];

    function formToJson() {
        let jsonSB = []; //using JS array like a string builder - hence why it ends with SB
        jsonSB.push('{ "numberGroups": [');
        if ($("#group1").prop('checked')) {
            jsonSB.push('{ "min": ' + $('#groupMin1').val() + ',');
            jsonSB.push('"max": ' + $('#groupMax1').val() + ',');
            jsonSB.push('"numbersPerGroup": ' + $('#numbersPerGroup1').val() + ',');
            jsonSB.push('"divergence": ' + $('#divergence1').val() + ',');
            jsonSB.push('"sumCheck": ' + $('#checkSum1').prop('checked') + ',');
            jsonSB.push('"oeCheck": ' + $('#checkOE1').prop('checked') + '}');
            grp1count = $("#numbersPerGroup1Val").val(); //this is for setting the colors and also for checking if no groups are selected
        };
        if ($("#group2").prop('checked')) {
            jsonSB.push(',{ "min": ' + $('#groupMin2').val() + ',');
            jsonSB.push('"max": ' + $('#groupMax2').val() + ',');
            jsonSB.push('"numbersPerGroup": ' + $('#numbersPerGroup2').val() + ',');
            jsonSB.push('"divergence": ' + $('#divergence2').val() + ',');
            jsonSB.push('"sumCheck": ' + $('#checkSum2').prop('checked') + ',');
            jsonSB.push('"oeCheck": ' + $('#checkOE2').prop('checked') + '}');
            grp2count = $("#numbersPerGroup2Val").val();
        };
        if ($("#group3").prop('checked')) {
            jsonSB.push(',{ "min": ' + $('#groupMin3').val() + ',');
            jsonSB.push('"max": ' + $('#groupMax3').val() + ',');
            jsonSB.push('"numbersPerGroup": ' + $('#numbersPerGroup3').val() + ',');
            jsonSB.push('"divergence": ' + $('#divergence3').val() + ',');
            jsonSB.push('"sumCheck": ' + $('#checkSum3').prop('checked') + ',');
            jsonSB.push('"oeCheck": ' + $('#checkOE3').prop('checked') + '}');
            grp3count = $("#numbersPerGroup3Val").val();
        };
        jsonSB.push('], "sets": ' + $('#numberOfSets').val() + '}');
        let finalJson = jsonSB.join('');
        return finalJson;
    }

    function updateSlider(slider, sliderText) {
        if (typeof (slider) !== 'undefined' || typeof (sliderText) !== 'undefined') {
            sliderText.val(slider.val());
            setTimeout(updateSlider, 50);
        }
    }

    $(".form-range").on("input", function (e) {
        updateSlider($(e.currentTarget), $("input[id=" + e.currentTarget.id + "Val]"));
    });


    let serviceUrl = "https://api.miraclecat.com/api/numbersets";
    //   let serviceUrl = "https://localhost:7238/api/numbersets";   //for testing

    function GetData(postdata) {
        colorsAry = []; //reset colors array
        // add colors to array to match the selected counts
        for (var j = 0; j < grp1count; j++) {
            colorsAry.push("#03a9f4");
        }
        for (var j = 0; j < grp2count; j++) {
            colorsAry.push("#e64a19");
        }
        for (var j = 0; j < grp3count; j++) {
            colorsAry.push("#aa00ff");
        }
        let i = 0; //for color array index

        if (grp1count + grp2count + grp3count > 0) {
            $('#results').empty();
            $.ajax({
                url: serviceUrl,
                type: "post",
                data: postdata,
                contentType: "application/json",
                success: function (result, status, xhr) {
                    let resultsDiv = $('#results');
                    result.forEach(function (lottoset) {
                        let innerDiv = $('<div></div>');
                        let aryNumSet = lottoset.toString().split(',');
                        i = 0; //reset color array index for each set
                        aryNumSet.forEach(function (singleNum) {
                            let tb = $('<span class="badge h2" style="background-color:' + colorsAry[i] + ';"></span>').text(singleNum);
                            innerDiv.append(tb);
                            i++;
                        });
                        resultsDiv.append(innerDiv);

                    });
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                }

            });


        } else {
            alert("No groups checked!");
        }



    };


});

