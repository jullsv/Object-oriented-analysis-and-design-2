from abc import ABC, abstractmethod

class File(ABC):
    def __init__(self, path: str):
        self._path = path
    
    @property
    def path(self) -> str:
        return self._path
    
    @abstractmethod
    def get_name(self) -> str:
        pass
    
    @abstractmethod
    def get_size(self) -> int:
        pass