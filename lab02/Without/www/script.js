let memeState = {
    decorators: []
};

async function apiRequest(endpoint, data) {
    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });
        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        return { error: error.message };
    }
}

function addText() {
    const text = document.getElementById('textInput').value;
    const position = document.getElementById('textPosition').value;
    const fontSize = document.getElementById('fontSize').value;
    const color = document.getElementById('textColor').value;
    
    if (!text) { alert('Введите текст!'); return; }
    
    memeState.decorators.push({
        type: 'text',
        text, position, fontSize: parseInt(fontSize), color
    });
    
    updatePreview();
    sendToServer();
}

function addFilter() {
    const filterType = document.getElementById('filterType').value;
    
    memeState.decorators.push({
        type: 'filter',
        filterType
    });
    
    updatePreview();
    sendToServer();
}

function addBorder() {
    const width = document.getElementById('borderWidth').value;
    const color = document.getElementById('borderColor').value;
    
    memeState.decorators.push({
        type: 'border',
        width: parseInt(width),
        color
    });
    
    updatePreview();
    sendToServer();
}

function addSticker() {
    const stickerPath = document.getElementById('stickerPath').value;
    const position = document.getElementById('stickerPosition').value;
    
    if (!stickerPath) { alert('Введите путь к стикеру!'); return; }
    
    memeState.decorators.push({
        type: 'sticker',
        stickerPath, position
    });
    
    updatePreview();
    sendToServer();
}

function updatePreview() {
    const overlays = document.getElementById('overlays');
    overlays.innerHTML = '';
    
    memeState.decorators.forEach(dec => {
        if (dec.type === 'text') {
            const el = document.createElement('div');
            el.textContent = dec.text;
            
           let positionStyles = '';
            if (dec.position === 'top') {
                 positionStyles = 'top: 20px; left: 50%; transform: translateX(-50%);';
            } else if (dec.position === 'bottom') {
                positionStyles = 'bottom: 20px; left: 50%; transform: translateX(-50%);';
            } else if (dec.position === 'center') {
                positionStyles = `
                     top: 50%;
                     left: 50%;
                     transform: translate(-50%, -50%);
                    display: flex;
                    align-items: center;
                    justify-content: center;
                `;
    }
            
            el.style.cssText = `
                position: absolute;
                ${positionStyles}
                font-size: ${dec.fontSize}px;
                color: ${dec.color};
                font-weight: bold;
                text-shadow: 2px 2px 4px rgba(0,0,0,0.5);
                white-space: nowrap;
                z-index: 10;
            `;
            overlays.appendChild(el);
            
        } else if (dec.type === 'sticker') {
            const el = document.createElement('img');
            el.src = dec.stickerPath;
            
            let stickerPosition = '';
            if (dec.position === 'top-left') {
                stickerPosition = 'top: 10px; left: 10px;';
            } else if (dec.position === 'top-right') {
                stickerPosition = 'top: 10px; right: 10px;';
            } else if (dec.position === 'center') {
                stickerPosition = 'top: 50%; left: 50%; transform: translate(-50%, -50%);';
            }
            
            el.style.cssText = `
                position: absolute;
                ${stickerPosition}
                width: 100px;
                z-index: 10;
            `;
            overlays.appendChild(el);
            
        } else if (dec.type === 'border') {
            document.getElementById('memePreview').style.border = 
                `${dec.width}px solid ${dec.color}`;
                
        } else if (dec.type === 'filter') {
            document.getElementById('baseImage').style.filter = 
                dec.filterType === 'grayscale' ? 'grayscale(100%)' :
                dec.filterType === 'sepia' ? 'sepia(100%)' :
                dec.filterType === 'vintage' ? 'sepia(50%) contrast(120%)' : 'none';
        }
    });
    
    document.getElementById('memeInfo').textContent = 
        JSON.stringify(memeState, null, 2);
}

async function sendToServer() {
    const result = await apiRequest('/api/meme', memeState);
    if (result.description) {
        console.log('Server:', result.description);
    }
}

async function resetMeme() {
    memeState.decorators = [];
    updatePreview();
    await apiRequest('/api/reset', {});
}

async function saveMeme() {
    const result = await apiRequest('/api/save', memeState);
    if (result.message) {
        alert(result.message);
    }
}

// Initialize
updatePreview();