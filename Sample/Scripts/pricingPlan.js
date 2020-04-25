//pricing plan code

$("#btnSetPromoCode").click(function () {OriginalPrice
    $('#divPromoMessage').html("");
    $('#divPromoMessage').removeClass("ErrorOccurs");
    var code = $("#txtPromoCode").val();    
    if (code != null && code != "" && code != undefined) {
        var PlanId = $("#PlanId").val();
        AddUserStats("Apply promo code", JSON.stringify({ PlanId: PlanId, PromoCode: code }))
        $.ajax({
            url: '/pricingplan/validatepromocode',
            data: { promoCode: code, PlanId: PlanId },
            cache: false,
            type: "GET",
            async: false,
            success: function (data) {
                if (data != null) {
                    $("#PromoId").val(data.PromoId);
                    $('#divPromoMessage').html(data.DiscountType + " discount was applied to your purchase today!");
                    $('#divPromoMessage').addClass("text-success");
                    //set value in hidden Fields
                    $("#PromoCode").val(data.PromoCode);
                    $("#OriginalPrice").val(data.OriginalPrice);
                    $("#PlanPricing").val(data.FinalAmount);
                    //-----------------------------      
                    $("#divPrice").html(" : $ " + parseFloat(data.OriginalPrice).toFixed(2));
                    $("#divDiscount").html(" : -$ " + parseFloat(data.Discount));
                    $("#divPayableAmount").html(" : $ " + parseFloat(data.FinalAmount));                    
                }
                else {
                    $('#divPromoMessage').html("Promo code is invalid.");
                    $('#divPromoMessage').addClass("ErrorOccurs");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#divPromoMessage').html("Promo code is invalid.");
                $('#divPromoMessage').addClass("ErrorOccurs");
            }
        });
    }
    else
    {
        $('#divPromoMessage').html("Please enter promo code.");
        $('#divPromoMessage').addClass("ErrorOccurs");
    }
})

$(".btnSelect").click(function () {
    clearFields();
    var id = $(this).attr("id")
    var pricing = id.split("-");
    var PlanName = pricing[0];
    var PlanId = pricing[1];
    var PlanType = pricing[2];
    var PlanPrice = pricing[3];
    AddUserStats("Select Plan", JSON.stringify({ PlanId: PlanId, PlanName: PlanName, PlanPricing: PlanPrice }))
    var userPlan = $("#userPlan").html().toLowerCase();
    console.log(userPlan);    
    if(userPlan === "gold" || PlanName.toLowerCase() === userPlan)
    {
        console.log("hide")
        $("#divPlans").addClass("hide");
        $("#divPlanMessage").removeClass("hide");
        $("#divSubscribedPlan").html(userPlan);

    }
    else if (parseFloat(PlanPrice) <= 0  )
    {
        
        $("#divPromo").removeClass("hide");
        $("#divPlanMessage").addClass("hide");
        //$("#btnPayNow").attr("disabled", "disabled");
    }
    else
    {
        $("#divPromo").removeClass("hide");
        $("#divPlans").removeClass("hide");
        $("#btnSetPromoCode").removeAttr("disabled");
        $("#btnPayNow").removeAttr("disabled");
        $("#divPlanMessage").addClass("hide");
    }

    $("#PlanPricing").val(PlanPrice);
    $("#PlanType").val(PlanType);
    $("#PlanName").val(PlanName);
    $("#PlanId").val(PlanId);
    $("#divPlan").html(PlanName);
    

    $("#divPrice").html(" : $ " +parseFloat(PlanPrice).toFixed(2));
    $("#divDiscount").html(" : -$ 0" );
    $("#divPayableAmount").html(" : $ " + parseFloat(PlanPrice).toFixed(2));
})


function clearFields()
{
    $("#PromoId").val();
    $('#divPromoMessage').html();
    //set value in hidden Fields
    $("#PromoCode").val();
    $("#OriginalPrice").val();
    $("#PlanPricing").val();
    //-----------------------------      
    $("#divPrice").html();
    $("#divDiscount").html();
    $("#divPayableAmount").html();
}
