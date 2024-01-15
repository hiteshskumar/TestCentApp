var specialChar=["()","{}","[]","''"];
 
window.insertAtCursor= function (id, myValue) {
    var myField = document.getElementById(id);
    var result="";
    //IE support
    if (document.selection) {
        console.log("document.selection");
        myField.focus();
        sel = document.selection.createRange();
        sel.text = myValue;
    }
    //MOZILLA and others
    else if (myField.selectionStart || myField.selectionStart == '0') {
        var startPos = myField.selectionStart;
        var endPos = myField.selectionEnd; 
        console.log("startPos :"+startPos+" startPos :"+startPos);
        if(startPos!=0 && specialChar.indexOf(myValue)>=0){
            startPos=startPos-1;
        } 
        result=myField.value.substring(0, startPos) + myValue + myField.value.substring(endPos, myField.value.length); 
        myField.value = result; 
        
        if(myValue==" @[]")
            endPos=endPos+3;
        
        myField.selectionEnd=endPos;        
    } else {
        myField.value += myValue;
        console.log("else");
    }
    return result;
}

var properties = [
    'boxSizing',
    'width', 
    'height',
    'overflowX',
    'overflowY', 

    'borderTopWidth',
    'borderRightWidth',
    'borderBottomWidth',
    'borderLeftWidth',

    'paddingTop',
    'paddingRight',
    'paddingBottom',
    'paddingLeft',

    // https://developer.mozilla.org/en-US/docs/Web/CSS/font
    'fontStyle',
    'fontVariant',
    'fontWeight',
    'fontStretch',
    'fontSize',
    'lineHeight',
    'fontFamily',

    'textAlign',
    'textTransform',
    'textIndent',
    'textDecoration',

    'letterSpacing',
    'wordSpacing'
];

var isFirefox = !(window.mozInnerScreenX === null);
var element, mirrorDiv, computed, style;

window.getCaretCoordinates= function (elName) {
        element = document.getElementById(elName); 

        mirrorDiv = document.getElementById(element.nodeName + '--mirror-div');
        if (!mirrorDiv) {
            mirrorDiv = document.createElement('div');
            mirrorDiv.id = element.nodeName + '--mirror-div';
            document.body.appendChild(mirrorDiv);
        }
       
        style = mirrorDiv.style;
      
        
        computed = getComputedStyle(element); 

        style.whiteSpace = 'pre-wrap';
        if (element.nodeName !== 'INPUT')
            style.wordWrap = 'break-word';  
       
        style.position = 'absolute'; 
        style.top = element.offsetTop + parseInt(computed.borderTopWidth) + 'px';
        style.left = "400px";
        style.visibility = 'hidden';  


        properties.forEach(function (prop) {
            style[prop] = computed[prop];
        });

        if (isFirefox) {
            style.width = parseInt(computed.width) - 2 + 'px'  
            if (element.scrollHeight > parseInt(computed.height))
                style.overflowY = 'scroll';
        } else {
            style.overflow = 'hidden';  
        }

        mirrorDiv.textContent = element.value.substring(0, element.selectionEnd);
       
        if (element.nodeName === 'INPUT')
            mirrorDiv.textContent = mirrorDiv.textContent.replace(/\s/g, "\u00a0");

        var span = document.createElement('span');
        
        span.textContent = element.value.substring(element.selectionEnd) || '.';  
        span.style.backgroundColor = "lightgrey";
        mirrorDiv.appendChild(span);


        var a = span.offsetTop + parseInt(computed['borderTopWidth']);
        var b = span.offsetLeft + parseInt(computed['borderLeftWidth']);

        var c = a + "," + b;


        document.body.removeChild(mirrorDiv);

        return c;
} 
window.GetElementActualTop =function (el) {
    if (document.getElementById(el) !== null) {
        let rect = document.getElementById(el).getBoundingClientRect();
        return rect.top;
    }
    else {
        return 0;
    }
}
window.GetElementActualLeft= function (el) {
    if (document.getElementById(el) !== null) {
        let rect = document.getElementById(el).getBoundingClientRect();
        return rect.left;
    }
    else {
        return 0;
    }
}
window.GetElementActualWidth= function (el) {
    if (document.getElementById(el) !== null) {
        var rect = document.getElementById(el);
        return rect.clientWidth;
    }
    else {
        return 0;
    }
}

window.OnTextAreaFocus=function(){
      window.scrollTo(0, 150);  
}
window.GetCaretPosition=function(id){
    var myField = document.getElementById(id);
    var position=0;
    //IE support
    if (document.selection) {
        console.log("document.selection");
        myField.focus();
        sel = document.selection.createRange();
        sel.text = myValue;
    }
    //MOZILLA and others
    else if (myField.selectionStart || myField.selectionStart == '0') {
        position=myField.selectionStart;
    }
    return position;
}

window.GetElementCurrentValue= function (id) {
    var myField = document.getElementById(id);
    return myField.value;
}
//let fclick;
//let fhandleKey;
let eRef;
let objRef;
let controlId; 
window.MultiSelect_RemoveListener= function (ref,objref,id) {
    const listener = listenerClickMap.get(objref._id);
    document.removeEventListener('click',listener);
    const listenerTab = listenerTabMap.get(objref._id);
    document.removeEventListener('keydown',listenerTab);
    objRef=undefined;
}
const listenerTabMap = new Map();
const listenerClickMap = new Map();
window.MultiSelect_Init= function (ref,e,id) 
{
    if(objRef != undefined)
    {
        objRef.invokeMethodAsync("MatDialogClosedHandler");
    }
    eRef = ref;
    objRef=e;
    controlId=id;
    const listener = fnClicked.bind(e._id);
    listenerClickMap.set(e._id, listener);
    document.addEventListener("click", listener); 
    const listenerTab = fnHandleKey.bind(e._id);
    listenerTabMap.set(e._id, listenerTab);
    document.addEventListener("keydown", listenerTab); 
}

function fnClicked(evt)
{
    if(objRef!=undefined)
    {
        try{
            var flyoutElement = document.getElementById(controlId),
                targetElement = evt.target;  // clicked element        

                if(targetElement && targetElement.classList.length>0 && !(targetElement.classList[0]=="autocomplete-input" || targetElement.classList[0]=="autocomplete"))
                {
                    objRef.invokeMethodAsync("MatDialogClosedHandler");
                    return;
                }
            do {
                if (targetElement == flyoutElement) { 
                    objRef.invokeMethodAsync("MatDialogOpenedHandler");
                    return;
                }
                // Go up the DOM
                targetElement = targetElement.parentNode;
            } while (targetElement);    
            // This is a click outside.
            //objRef.invokeMethodAsync("MatDialogClosedHandler");
        }catch(er){
            return;
        }
    }
}
fnHandleKey=function(ev) 
{
    if(objRef!=undefined)
    {
        if (ev.keyCode === 9 || ev.keyCode === 13 || ev.keyCode === 27)
        {
            objRef.invokeMethodAsync("MatDialogClosedHandler");
        }
    }
}
changeBodyScroll = function () {
    document.body.className = " "+document.body.className.replace("mdc-dialog-scroll-lock","");
}
OnSetFocus = function(elementID)
{
    document.getElementById(elementID).focus();
}
