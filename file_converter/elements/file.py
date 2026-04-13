from abc import ABC, abstractmethod
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from visitors.file_visitor import FileVisitor

class File(ABC):
    def __init__(self, path: str):
        self._path = path
    
    @property
    def path(self) -> str:
        return self._path
    
    @abstractmethod
    def accept(self, visitor: 'FileVisitor') -> None:
        pass
    
    @abstractmethod
    def get_name(self) -> str:
        pass
    
    @abstractmethod
    def get_size(self) -> int:
        pass