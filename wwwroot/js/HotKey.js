let componentRef;
window.OnHotKey_Init= function(ref)
{
    componentRef=ref;
}

document.onkeydown = function (arg) 
{
    //console.log(arg);
    componentRef.invokeMethodAsync("OnKeyDownEvent",arg.ctrlKey,arg.altKey,arg.shiftKey,arg.code);
}