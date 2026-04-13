from abc import ABC, abstractmethod
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from elements.image_file import ImageFile
    from elements.document_file import DocumentFile

class FileVisitor(ABC):
    def __init__(self, output_path: str):
        self._output_path = output_path
    
    @property
    def output_path(self) -> str:
        return self._output_path
    
    @abstractmethod
    def visit_image_file(self, file: 'ImageFile') -> None:
        pass
    
    @abstractmethod
    def visit_document_file(self, file: 'DocumentFile') -> None:
        pass