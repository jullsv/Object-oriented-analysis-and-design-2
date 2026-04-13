from .file import File

class ImageFile(File):
    def __init__(self, path: str, width: int, height: int, format: str):
        super().__init__(path)
        self._width = width
        self._height = height
        self._format = format
    
    @property
    def width(self) -> int:
        return self._width
    
    @property
    def height(self) -> int:
        return self._height
    
    @property
    def format(self) -> str:
        return self._format
    
    def get_name(self) -> str:
        return f"Image: {self._path}"
    
    def get_size(self) -> int:
        return self._width * self._height * 3